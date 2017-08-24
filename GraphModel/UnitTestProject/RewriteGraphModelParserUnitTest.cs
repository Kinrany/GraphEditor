using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphModelLibrary.Rewrite;

namespace UnitTestProject {
	[TestClass]
	public class RewriteGraphModelParserUnitTest {
		[TestMethod]
		public void Parsing1() {
			string text =
@"5
0 1 0 1 1
1 0 0 1 1
1 0 0 0 1
1 0 1 0 1
1 1 1 1 0";

			GraphModel model = GraphModelParser.ParseA1(text);
		}

		[TestMethod]
		public void Parsing2() {
			string text =
@"3
0 2 3
-1 0 0
5 42 0
Node colors:
1 0 3
Text:
Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Morbi elementum lorem et libero bibendum, ac egestas urna accumsan.";

			GraphModel model = GraphModelParser.ParseA1(text);
		}

		[TestMethod]
		public void Parsing3() {
			string text =
@"4
0 2 3 100
-1 0 0 -1
5 42 0 24
1 1 -10 0
Edge colors:
0 1 5
0 2 3
-1
Node colors:
1 0 3
Text:
Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Morbi elementum lorem et libero bibendum, ac egestas urna accumsan.";

			GraphModel model = GraphModelParser.ParseA1(text);
		}

		[TestMethod]
		public void Parsing4_EmptyGraph() {
			string text =
@"0
";

			GraphModel graph = GraphModelParser.ParseA1(text);

			Assert.IsTrue(graph.NodeCount == 0);
			Assert.IsTrue(graph.EdgeCount == 0);
		}

		[TestMethod]
		public void Parsing4_TextOnly() {
			string text =
@"0
Text:
Hello world!
What's up?";

			GraphModel graph = GraphModelParser.ParseA1(text);

			string expectedText =
@"Hello world!
What's up?
";

			Assert.IsTrue(graph.Text == expectedText);
		}

		[TestMethod]
		public void Parsing5_ColoredNode() {
			string text =
@"1
0
Node colors:
5";

			GraphModel graph = GraphModelParser.ParseA1(text);
			NodeModel node = new NodeModel(graph, graph.NodeEnumerator.First());

			Assert.IsTrue(node.ColorId == 5);
		}

		[TestMethod]
		public void Parsing6_ColoredEdge() {
			string text =
@"2
0 1
0 0
Edge colors:
0 1 5
-1";

			GraphModel graph = GraphModelParser.ParseA1(text);
			NodeModel node1 = new NodeModel(graph, graph.NodeEnumerator.First());
			NodeModel node2 = new NodeModel(graph, graph.NodeEnumerator.Skip(1).First());
			EdgeModel edge = EdgeModel.Between(node1, node2);

			Assert.IsTrue(edge.ColorId == 5);
		}

		[TestMethod]
		public void Loading1() {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-1.txt");
			GraphModel model = GraphModelParser.Load(path);
		}

		[TestMethod]
		public void Loading2() {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-2.txt");
			GraphModel model = GraphModelParser.Load(path);
		}

		[TestMethod]
		public void Loading3() {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-3.txt");
			GraphModel model = GraphModelParser.Load(path);
		}
		
		[TestMethod]
		public void Serializing1_EmptyGraph() {
			GraphModel graph = new GraphModel();

			string[] serialized = GraphModelParser.SerializeA1(graph);

			Assert.IsTrue(serialized[0] == "0");
		}

		[TestMethod]
		public void Serializing2_SingleNode() {
			GraphModel graph = new GraphModel();
			NodeIndex nodeIndex = graph.CreateNode();

			string[] serialized = GraphModelParser.SerializeA1(graph);

			Assert.IsTrue(serialized[0] == "1");
		}

		[TestMethod]
		public void Serializing3_SingleEdge() {
			GraphModel graph = new GraphModel();

			NodeIndex nodeIndex = graph.CreateNode();
			var nodeWeight = new GraphModel.NodeWeight(nodeIndex);
			graph.SetNodeWeight(nodeIndex, nodeWeight);

			EdgeIndex edgeIndex = graph.CreateEdge(nodeIndex, nodeIndex);
			var edgeWeight = GraphModel.EdgeWeight.DEFAULT;
			edgeWeight.Value = "1";
			graph.SetEdgeWeight(edgeIndex, edgeWeight);

			string[] serialized = GraphModelParser.SerializeA1(graph);
			
			Assert.IsTrue(serialized[1] == "1");
		}

		[TestMethod]
		public void Serializing4_TextOnly() {
			GraphModel graph = new GraphModel();
			string text = 
@"Hello world!
How are you?

I'm fine too!";
			graph.Text = text;

			string[] serialized = GraphModelParser.SerializeA1(graph);

			serialized = serialized
				.SkipWhile((str) => str != "Text:")
				.Skip(1)
				.ToArray();
			string[] textLines = new string[] {
				"Hello world!",
				"How are you?",
				"",
				"I'm fine too!"
			};
			Assert.IsTrue(serialized.Length == textLines.Length);
			for (int i = 0; i < serialized.Length; ++i) {
				Assert.IsTrue(serialized[i] == textLines[i]);
			}
		}

		[TestMethod]
		public void Serializing5_MissingNodeNames() {
			GraphModel graph = new GraphModel();

			NodeModel node1 = NodeModel.Create(graph);
			NodeModel node2 = NodeModel.Create(graph);
			node1.Delete();

			string[] serialized = GraphModelParser.SerializeA1(graph);

			string[] expected = new string[] {
				"1",
				"0",
				"Node colors:",
				"0",
				"Edge colors:",
				"-1"
			};

			Assert.AreEqual(serialized.Length, expected.Length);
			for (int i = 0; i < serialized.Length; ++i) {
				Assert.AreEqual(serialized[i], expected[i]);
			}
		}

		[TestMethod]
		public void Serializing6_ColoredNode() {
			GraphModel graph = new GraphModel();
			NodeModel node = NodeModel.Create(graph);
			node.ColorId = new ColorId(5);

			string[] serialized = GraphModelParser.SerializeA1(graph);

			string[] expected = new string[] {
				"1",
				"0",
				"Node colors:",
				"5",
				"Edge colors:",
				"-1"
			};

			Assert.AreEqual(serialized.Length, expected.Length);
			for (int i = 0; i < serialized.Length; ++i) {
				Assert.AreEqual(serialized[i], expected[i]);
			}
		}

		[TestMethod]
		public void Serializing7_ColoredEdge() {
			GraphModel graph = new GraphModel();
			NodeModel node1 = NodeModel.Create(graph);
			NodeModel node2 = NodeModel.Create(graph);
			EdgeModel edge = EdgeModel.Create(node1, node2);
			edge.Value = "1";
			edge.ColorId = new ColorId(5);

			string[] serialized = GraphModelParser.SerializeA1(graph);

			string[] expected = new string[] {
				"2",
				"0 1",
				"0 0",
				"Node colors:",
				"0 0",
				"Edge colors:",
				"0 1 5",
				"-1"
			};

			Assert.AreEqual(serialized.Length, expected.Length);
			for (int i = 0; i < serialized.Length; ++i) {
				Assert.AreEqual(serialized[i], expected[i]);
			}
		}

		[TestMethod]
		public void Serializing8_MissingNodesWithEdges() {
			GraphModel graph = new GraphModel();

			NodeModel node1 = NodeModel.Create(graph);
			NodeModel node2 = NodeModel.Create(graph);
			NodeModel node3 = NodeModel.Create(graph);
			EdgeModel edge = EdgeModel.Create(node2, node3);
			node1.Delete();

			string[] serialized = GraphModelParser.SerializeA1(graph);

			string[] expected = new string[] {
				"2",
				"0 0",
				"0 0",
				"Node colors:",
				"0 0",
				"Edge colors:",
				"0 1 0",
				"-1"
			};

			Assert.AreEqual(serialized.Length, expected.Length);
			for (int i = 0; i < serialized.Length; ++i) {
				Assert.AreEqual(serialized[i], expected[i]);
			}
		}

		[TestMethod]
		public void Saving1() {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-3.txt");
			GraphModel model = GraphModelParser.Load(path);
			string savePath = @"~tempfile.txt";
			GraphModelParser.Save(model, savePath);
			try {
				GraphModel model2 = GraphModelParser.Load(savePath);
			}
			finally {
				File.Delete(savePath);
			}
		}
	}
}
