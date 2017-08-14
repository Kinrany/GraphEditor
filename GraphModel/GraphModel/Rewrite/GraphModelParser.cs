using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ExtensionMethods;

namespace GraphModelLibrary.Rewrite {
	public static class GraphModelParser {
		/// <summary>
		/// Загружает граф из файла.
		/// </summary>
		/// <param name="path">Путь к файлу в файловой системе.</param>
		/// <returns>Объект графа.</returns>
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
						int[] colorNumbers = Helper.StringToIntArray(queue.Dequeue());
						Color[] colors = colorNumbers.Map(number => Helper.IntToColor[number]);

						for (int i = 0; i < colors.Length; ++i) {
							NodeModel node = new NodeModel(graph, nodeIndices[i]);
							node.Color = colors[i];
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

							Color color = Helper.IntToColor[colorNumber];

							EdgeIndex edgeIndex = edgeIndices[nodeFromIndex, nodeToIndex];

							var weight = graph.GetEdgeWeight(edgeIndex);
							weight.Color = color;
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

		public static void Save(GraphModel graph, string path) {
			File.WriteAllLines(path, GraphModelParser.SerializeA1(graph));
		}

		public static string[] SerializeA1(GraphModel graph) {
			List<string> text = new List<string>();

			// номера вершин должны быть от 0 до n-1
			graph.Reindex();

			// количество вершин
			int N = graph.NodeCount;
			text.Add(N.ToString());

			// матрица весов рёбер
			for (int i = 0; i < N; ++i) {
				string[] edgeValues = new string[N];

				for (int j = 0; j < N; ++j) {
					EdgeIndex? edgeIndex = graph.GetEdgeBetween(new NodeIndex(i), new NodeIndex(j));

					if (edgeIndex != null) {
						edgeValues[j] = graph.GetEdgeWeight((EdgeIndex)edgeIndex).Value;
					}
					else {
						edgeValues[j] = "0";
					}
				}

				text.Add(string.Join(" ", edgeValues));
			}

			// цвета вершин
			text.Add("Node colors:");
			string[] colorNumbers = new string[N];
			for (int i = 0; i < N; ++i) {
				Color color = graph.GetNodeWeight(new NodeIndex(i)).Color;
				colorNumbers[i] = Helper.ColorToInt[color].ToString();
			}
			text.Add(string.Join(" ", colorNumbers));

			// цвета рёбер, по одному ребру на строке
			text.Add("Edge colors:");
			foreach (EdgeIndex edgeIndex in graph.EdgeEnumerator) {
				int nodeFromIndex = graph.GetNodeFrom(edgeIndex);
				int nodeToIndex = graph.GetNodeTo(edgeIndex);
				Color edgeColor = graph.GetEdgeWeight(edgeIndex).Color;
				int colorNumber = Helper.ColorToInt[edgeColor];
				string str = string.Format("{0} {1} {2}", nodeFromIndex, nodeToIndex, colorNumber);
			}
			text.Add("-1");

			// текст, если есть
			if (graph.Text != null && graph.Text != "") {
				text.Add("Text:");
				text.AddRange(graph.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None));
			}

			return text.ToArray();
		}
	}

	public static class Helper {

		static Helper() {
			// заполняем ColorToInt в соответствии с IntToColor
			ColorToInt = new Dictionary<Color, int>();
			for (int i = 0; i < IntToColor.Length; ++i) {
				Color color = IntToColor[i];
				if (color != null) {
					ColorToInt[color] = i;
				}
			}
		}

		// Статическая таблица цветов
		public static Dictionary<Color, int> ColorToInt;
		public static Color[] IntToColor = {
			Color.Magenta,
			Color.Blue,
			Color.Red,
			Color.Green,
			Color.Black,
			Color.White,
			Color.Purple,
			Color.Orange,
			Color.Yellow,
			Color.Pink,
			Color.Turquoise,
			Color.Gray,
			Color.Teal,
			Color.DarkBlue,
			Color.Violet,
			Color.Brown
		};

		/// <summary>
		/// Разбивает строку на числа.
		/// </summary>
		/// <param name="str">Строка, содержащая числа, разбитые пробелами.</param>
		/// <returns>Массив чисел.</returns>
		public static int[] StringToIntArray(string str) {
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
	}
}
