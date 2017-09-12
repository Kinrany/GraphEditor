using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ExtensionMethods;

namespace GraphModelLibrary.Rewrite {
	public static class GraphModelParser {
		/// <summary>
		/// Загружает граф из файла.
		/// </summary>
		/// <param name="path">Путь к файлу в файловой системе.</param>
		/// <returns>Объект графа.</returns>
		[Obsolete]
		public static GraphModel Load(string path) {
			string text = File.ReadAllText(path);
			return GraphModelParser.ParseA1(text);
		}

		/// <summary>
		/// Парсит строку, описывающую граф в формате A1 (см. вики).
		/// </summary>
		/// <param name="str">Строка с описанием графа.</param>
		/// <returns>Объект графа.</returns>
		public static GraphModel ParseA1(string str) {
			var graph = new GraphModel();

			// разобьём на строки и уберём пустые, оставшиеся сложим в очередь
			char[] separators = { '\r', '\n' };
			string[] lines = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			Queue<string> queue = new Queue<string>(lines);

			try {
				// количество точек
				int n = int.Parse(queue.Dequeue());

				// создадим точки
				NodeIndex[] nodeIndices = new NodeIndex[n];
				for (int i = 0; i < n; ++i) {
					NodeIndex nodeIndex = graph.CreateNode();
					var weight = new GraphModel.NodeWeight(nodeIndex);
					graph.SetNodeWeight(nodeIndex, weight);
					nodeIndices[i] = nodeIndex;
				}

				// создадим рёбра с заданными весами
				EdgeIndex[,] edgeIndices = new EdgeIndex[n, n];
				for (int i = 0; i < n; ++i) {
					string line = queue.Dequeue();
					int[] weightValues = Helper.StringToIntArray(line);

					for (int j = 0; j < n; ++j) {
						int weightValue = weightValues[j];

						EdgeIndex index = graph.CreateEdge(nodeIndices[i], nodeIndices[j]);
						var weight = GraphModel.EdgeWeight.DEFAULT;
						weight.Value = weightValue.ToString();
						graph.SetEdgeWeight(index, weight);

						edgeIndices[i, j] = index;
					}
				}

				// перебираем оставшиеся необязательные элементы файла
				while (queue.Count != 0) {
					string line = queue.Dequeue();

					switch (line) {

					// строка с цветами вершин
					case "Node colors:":
						int[] colorIds = Helper.StringToIntArray(queue.Dequeue());

						for (int i = 0; i < colorIds.Length; ++i) {
							NodeModel node = new NodeModel(graph, nodeIndices[i]);
							node.ColorId = new ColorId(colorIds[i]);
						}

						break;

					// строки с номерами вершин и цветами рёбер между ними
					case "Edge colors:":
						line = queue.Dequeue();
						while (line != "-1") {
							int[] numbers = Helper.StringToIntArray(line);
							int nodeFromIndex = numbers[0];
							int nodeToIndex = numbers[1];
							int colorNumber = numbers[2];

							ColorId colorId = new ColorId(colorNumber);

							EdgeIndex edgeIndex = edgeIndices[nodeFromIndex, nodeToIndex];

							var weight = graph.GetEdgeWeight(edgeIndex);
							weight.ColorId = colorId;
							graph.SetEdgeWeight(edgeIndex, weight);

							line = queue.Dequeue();
						}

						break;

					// текст в конце файла
					case "Text:":
						StringBuilder sb = new StringBuilder();
						while (queue.Count > 0) {
							sb.AppendLine(queue.Dequeue());
						}
						string text = sb.ToString();
						graph.Text = text;

						break;
					}
				}

				return graph;
			}
			catch (Exception e) {
				throw new InvalidDataException("Неправильный формат входных данных", e);
			}
		}

		public static GraphModel ParseA2(string str) {

			// wrap the input string in a reader
			StringReader reader = new StringReader(str);

			// create new graph object
			GraphModel graph = new GraphModel();

			// create dictionaries for mapping node and
			// edge numbers to nodes and edges in graph
			var nodes = new Dictionary<int, NodeModel>();
			var edges = new Dictionary<int, EdgeModel>();

			// read N and M
			int N = Helper.ReadInt(reader);
			int M = Helper.ReadInt(reader);

			// read nodes
			{
				int[] n = new int[N];
				for (int i = 0; i < N; ++i) {
					n[i] = Helper.ReadInt(reader);
				}

				// create new nodes
				for (int i = 0; i < N; ++i) {
					var node = NodeModel.Create(graph);
					node.Name = n[i].ToString();
					nodes[n[i]] = node;
				}
			}

			// read edges
			{
				int[] m = new int[M];
				int[] f = new int[M];
				int[] t = new int[M];
				for (int i = 0; i < M; ++i) {
					m[i] = Helper.ReadInt(reader);
					f[i] = Helper.ReadInt(reader);
					t[i] = Helper.ReadInt(reader);
				}

				// create new edges
				for (int i = 0; i < M; ++i) {
					var nodeFrom = nodes[f[i]];
					var nodeTo = nodes[t[i]];
					var edge = EdgeModel.Create(nodeFrom, nodeTo);
					edges[m[i]] = edge;
				}
			}

			// read file description flags
			bool edgeWeights = Helper.ReadChar(reader) == '+';
			bool nodeColors = Helper.ReadChar(reader) == '+';
			bool edgeColors = Helper.ReadChar(reader) == '+';
			bool includeText = Helper.ReadChar(reader) == '+';

			// read edge weights if present
			if (edgeWeights) {
				for (int i = 0; i < M; ++i) {
					int m = Helper.ReadInt(reader);
					string w = Helper.ReadWord(reader);
					edges[m].Value = w;
				}
			}

			// read node colors if present
			if (nodeColors) {
				for (int i = 0; i < N; ++i) {
					int n = Helper.ReadInt(reader);
					int c = Helper.ReadInt(reader);
					nodes[n].ColorId = new ColorId(c);
				}
			}

			// read edge colors if present
			if (edgeColors) {
				for (int i = 0; i < M; ++i) {
					int m = Helper.ReadInt(reader);
					int c = Helper.ReadInt(reader);
					edges[m].ColorId = new ColorId(c);
				}
			}

			// read text if present
			Helper.SkipWhitespace(reader);
			graph.Text = reader.ReadToEnd();

			return graph;
		}

		[Obsolete]
		public static void SaveA1(GraphModel graph, string path) {
			File.WriteAllLines(path, GraphModelParser.SerializeA1(graph));
		}

		public static string[] SerializeA1(GraphModel graph) {
			List<string> text = new List<string>();

			// названия вершин должны быть от 0 до n-1
			graph.Reindex();

			// количество вершин
			int N = graph.NodeCount;
			text.Add(N.ToString());

			// матрица весов рёбер
			for (int i = 0; i < N; ++i) {
				NodeModel nodeFrom = graph.GetNodeByName(i.ToString());

				string[] edgeValues = new string[N];

				for (int j = 0; j < N; ++j) {
					NodeModel nodeTo = graph.GetNodeByName(j.ToString());

					EdgeModel edge = EdgeModel.Between(nodeFrom, nodeTo);

					if (!string.IsNullOrWhiteSpace(edge?.Value)) {
						edgeValues[j] = edge.Value;
					}
					else {
						edgeValues[j] = "0";
					}
				}

				text.Add(string.Join(" ", edgeValues));
			}

			if (N > 0) {
				// цвета вершин
				text.Add("Node colors:");
				string[] colorNumbers = new string[N];
				for (int i = 0; i < N; ++i) {
					NodeModel node = graph.GetNodeByName(i.ToString());
					colorNumbers[i] = node.ColorId.ToString();
				}
				text.Add(string.Join(" ", colorNumbers));


				// цвета рёбер, по одному ребру на строке
				text.Add("Edge colors:");
				foreach (EdgeIndex edgeIndex in graph.EdgeEnumerator) {
					NodeModel nodeFrom = new NodeModel(graph, graph.GetNodeFrom(edgeIndex));
					NodeModel nodeTo = new NodeModel(graph, graph.GetNodeTo(edgeIndex));
					ColorId edgeColorId = graph.GetEdgeWeight(edgeIndex).ColorId;
					string str = string.Format("{0} {1} {2}", nodeFrom.Name, nodeTo.Name, edgeColorId);
					text.Add(str);
				}
				text.Add("-1");
			}

			// текст, если есть
			if (!string.IsNullOrEmpty(graph.Text)) {
				text.Add("Text:");
				text.AddRange(graph.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None));
			}

			return text.ToArray();
		}

		public static string SerializeA2(GraphModel graph, bool edgeWeights = true, bool nodeColors = true, bool edgeColors = true, bool includeText = true) {
			StringBuilder output = new StringBuilder();

			// N и M
			// Число вершин и число рёбер
			int N = graph.NodeCount;
			int M = graph.EdgeCount;
			output.AppendFormat("{0} {1}", N, M);
			output.AppendLine();

			NodeModel[] nodes = NodeModel.Enumerate(graph).ToArray();
			Debug.Assert(nodes.Length == N);
			EdgeModel[] edges = EdgeModel.Enumerate(graph).ToArray();
			Debug.Assert(edges.Length == M);

			// Строка n1 ... nN
			// Номера вершин
			int[] n = nodes.Select(node => node.Index.Value).ToArray();
			output.Append(string.Join(" ", n));
			output.AppendLine();

			// M строк с числами m, f, t
			// m - номер ребра
			// f - номер начальной вершины
			// t - номер конечной вершины
			int[] m = edges.Select(edge => edge.Index.Value).ToArray();
			int[] f = edges.Select(e => e.NodeFrom)
				.Select(node => node.Index.Value).ToArray();
			int[] t = edges.Select(e => e.NodeTo)
				.Select(node => node.Index.Value).ToArray();
			for (int i = 0; i < M; ++i) {
				output.AppendFormat("{0} {1} {2}", m[i], f[i], t[i]);
				output.AppendLine();
			}

			// Строка со знаками + или -
			bool[] flags = new bool[] { edgeWeights, nodeColors, edgeColors, includeText };
			char[] flagChars = flags.Select(flag => flag ? '+' : '-').ToArray();
			output.Append(string.Join(" ", flagChars));
			output.AppendLine();

			// Веса рёбер
			if (edgeWeights) {
				string[] w = edges.Select(edge => edge.Value).ToArray();
				for (int i = 0; i < M; ++i) {
					Debug.Assert(!Regex.IsMatch(w[i], @"\s"));
					output.AppendFormat("{0} {1}", m[i], w[i]);
					output.AppendLine();
				}
			}

			// Цвета вершин
			if (nodeColors) {
				int[] c = nodes.Select(node => node.ColorId.Value).ToArray();
				for (int i = 0; i < N; ++i) {
					output.AppendFormat("{0} {1}", n[i], c[i]);
					output.AppendLine();
				}
			}

			// Цвета рёбер
			if (edgeColors) {
				int[] c = edges.Select(edge => edge.ColorId.Value).ToArray();
				for (int i = 0; i < M; ++i) {
					output.AppendFormat("{0} {1}", m[i], c[i]);
					output.AppendLine();
				}
			}

			// Текст
			if (includeText) {
				string text = graph.Text;
				output.Append(text);
			}

			return output.ToString();
		}
	}

	public static class Helper {

		/// <summary>
		/// Разбивает строку на числа.
		/// </summary>
		/// <param name="str">Строка, содержащая числа, разбитые пробелами.</param>
		/// <returns>Массив чисел.</returns>
		public static int[] StringToIntArray(string str) {
			if (string.IsNullOrWhiteSpace(str)) {
				return new int[0];
			}

			return str.Split().Map(x => int.Parse(x));
		}

		/// <summary>
		/// Вычисляет координаты точки на окружности.
		/// </summary>
		/// <param name="n">Количество точек.</param>
		/// <param name="i">Номер точки.</param>
		/// <returns></returns>
		public static Point IndexToPoint(int n, int i) {
			Point middle = new Point(400, 200);
			double angle = Math.PI * 2 * i / n;
			int radius = 50;

			float x = middle.X + (float)Math.Cos(angle) * radius;
			float y = middle.Y + (float)Math.Sin(angle) * radius;
			return Point.Round(new PointF(x, y));
		}

		/// <summary>
		/// Reads the first integer from a TextReader separated by whitespaces.
		/// </summary>
		/// <param name="reader">TextReader to read from.</param>
		/// <returns>The integer.</returns>
		public static int ReadInt(TextReader reader) {

			SkipWhitespace(reader);

			StringBuilder sb = new StringBuilder();

			while (!char.IsWhiteSpace((char)reader.Peek())) {
				sb.Append((char)reader.Read());
			}

			int result;
			if (int.TryParse(sb.ToString(), out result)) {
				return result;
			}
			else {
				throw new InvalidOperationException("Invalid input: not a valid integer");
			}
		}

		/// <summary>
		/// Reads the first string without whitespaces from a TextReader.
		/// </summary>
		/// <param name="reader">TextReader to read from.</param>
		/// <returns>The word.</returns>
		public static string ReadWord(TextReader reader) {

			SkipWhitespace(reader);

			StringBuilder sb = new StringBuilder();

			while (!char.IsWhiteSpace((char)reader.Peek())) {
				sb.Append((char)reader.Read());
			}

			return sb.ToString();
		}

		/// <summary>
		/// Reads the first non-whitespace character from a TextReader.
		/// </summary>
		/// <param name="reader">TextReader to read from.</param>
		/// <returns>The character.</returns>
		public static char ReadChar(TextReader reader) {

			SkipWhitespace(reader);

			return (char)reader.Read();
		}

		/// <summary>
		/// Skips all whitespace characters in the given TextReader.
		/// </summary>
		/// <param name="reader">TextReader to skip whitespaces in.</param>
		public static void SkipWhitespace(TextReader reader) {
			while (char.IsWhiteSpace((char)reader.Peek())) {
				reader.Read();
			}
		}
	}
}
