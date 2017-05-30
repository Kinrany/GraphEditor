using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ExtensionMethods;

namespace GraphModelLibrary.Rewrite {
	public class GraphModelParser {
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
				for (int i = 0; i < n; ++i) {
					graph.CreateNode(new GraphModel.NodeWeight());
				}

				// создадим рёбра с заданными весами
				for (int i = 0; i < n; ++i) {
					string line = queue.Dequeue();
					int[] numbers = Helper.StringToIntArray(line);

					for (int j = 0; j < n; ++j) {
						int weight = numbers[j];
						graph.CreateEdge(i, j, new GraphModel.EdgeWeight(weight.ToString()));
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
							for (int nodeIndex = 0; nodeIndex < colors.Length; ++nodeIndex) {
								graph.GetNodeWeight(nodeIndex).Color = colors[nodeIndex];
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

								int? edgeIndex = graph.GetEdgeBetween(nodeFromIndex, nodeToIndex);
								graph.GetEdgeWeight((int)edgeIndex).Color = color;

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
					int? edgeIndex = graph.GetEdgeBetween(i, j);

					if (edgeIndex != null) {
						edgeValues[j] = graph.GetEdgeWeight((int)edgeIndex).Value;
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
				Color color = graph.GetNodeWeight(i).Color;
				colorNumbers[i] = Helper.ColorToInt[color].ToString();
			}
			text.Add(string.Join(" ", colorNumbers));

			// цвета рёбер, по одному ребру на строке
			text.Add("Edge colors:");
			foreach (int edgeIndex in graph.EdgeEnumerator) {
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
