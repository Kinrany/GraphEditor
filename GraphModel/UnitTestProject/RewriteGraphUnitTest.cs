using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphModelLibrary.Rewrite;

namespace UnitTestProject {
	[TestClass]
	public class RewriteGraphUnitTest {
		[TestMethod]
		public void DeletingNode1() {
			Graph graph = new Graph();
			int index = graph.CreateNode();

			graph.DeleteNode(index);

			Assert.IsTrue(graph.NodeCount == 0);
		}

		[TestMethod]
		public void DeletingNode2() {
			Graph graph = new Graph();

			int index0 = graph.CreateNode();
			int index1 = graph.CreateNode();
			graph.DeleteNode(index0);
			int index2 = graph.CreateNode();
			int index3 = graph.CreateNode();
			graph.DeleteNode(index2);
			int index4 = graph.CreateNode();
			graph.DeleteNode(index1);

			Assert.IsTrue(graph.NodeCount == 2);
			Assert.IsTrue(graph.ContainsNode(index3));
			Assert.IsTrue(graph.ContainsNode(index4));
		}

		[TestMethod]
		public void DeletingNode3() {
			Graph graph = new Graph();
			int nodeIndex0 = graph.CreateNode();
			int nodeIndex1 = graph.CreateNode();
			int edgeIndex0 = graph.CreateEdge(nodeIndex0, nodeIndex1);
			int edgeIndex1 = graph.CreateEdge(nodeIndex0, nodeIndex0);
			int edgeIndex2 = graph.CreateEdge(nodeIndex1, nodeIndex1);
			int edgeIndex3 = graph.CreateEdge(nodeIndex1, nodeIndex0);

			graph.DeleteNode(nodeIndex0);

			Assert.IsTrue(graph.NodeCount == 1);
			Assert.IsTrue(graph.EdgeCount == 1);
			Assert.IsFalse(graph.ContainsEdge(edgeIndex0));
			Assert.IsFalse(graph.ContainsEdge(edgeIndex1));
			Assert.IsTrue(graph.ContainsEdge(edgeIndex2));
			Assert.IsFalse(graph.ContainsEdge(edgeIndex3));
		}
	}
}
