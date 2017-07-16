using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphModelLibrary.Rewrite;

namespace UnitTestProject {
	[TestClass]
	public class RewriteGraphUnitTest {
		[TestMethod]
		public void DeletingNode1() {
			Graph graph = new Graph();
			NodeIndex index = graph.CreateNode();

			graph.DeleteNode(index);

			Assert.IsTrue(graph.NodeCount == 0);
		}

		[TestMethod]
		public void DeletingNode2() {
			Graph graph = new Graph();

			NodeIndex index0 = graph.CreateNode();
			NodeIndex index1 = graph.CreateNode();
			graph.DeleteNode(index0);
			NodeIndex index2 = graph.CreateNode();
			NodeIndex index3 = graph.CreateNode();
			graph.DeleteNode(index2);
			NodeIndex index4 = graph.CreateNode();
			graph.DeleteNode(index1);

			Assert.IsTrue(graph.NodeCount == 2);
			Assert.IsTrue(graph.ContainsNode(index3));
			Assert.IsTrue(graph.ContainsNode(index4));
		}

		[TestMethod]
		public void DeletingNode3() {
			Graph graph = new Graph();
			NodeIndex nodeIndex0 = graph.CreateNode();
			NodeIndex nodeIndex1 = graph.CreateNode();
			EdgeIndex edgeIndex0 = graph.CreateEdge(nodeIndex0, nodeIndex1);
			EdgeIndex edgeIndex1 = graph.CreateEdge(nodeIndex0, nodeIndex0);
			EdgeIndex edgeIndex2 = graph.CreateEdge(nodeIndex1, nodeIndex1);
			EdgeIndex edgeIndex3 = graph.CreateEdge(nodeIndex1, nodeIndex0);

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
