﻿using System;
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

		public static void Save(GraphModel graph, string path) {
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

			// текст, если есть
			if (!string.IsNullOrEmpty(graph.Text)) {
				text.Add("Text:");
				text.AddRange(graph.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None));
			}

			return text.ToArray();
		}
	}

	public static class Helper {

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
