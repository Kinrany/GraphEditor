using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Color = System.Drawing.Color;

namespace GraphModelLibrary.Rewrite {
	public class WeightedGraph<TNode, TEdge> {

		public WeightedGraph() {
			_graph = new Graph();
			_nodeWeights = new Dictionary<int, TNode>();
			_edgeWeights = new Dictionary<int, TEdge>();
		}
		
		public int CreateNode(TNode weight) {
			int index = _graph.CreateNode();
			_nodeWeights[index] = weight;
			return index;
		}
		
		public int CreateEdge(int nodeFromIndex, int nodeToIndex, TEdge weight) {
			int edgeIndex = _graph.CreateEdge(nodeFromIndex, nodeToIndex);
			_edgeWeights[edgeIndex] = weight;
			return edgeIndex;
		}

		public void DeleteNode(int nodeIndex) {
			_nodeWeights.Remove(nodeIndex);
			_graph.DeleteNode(nodeIndex);
		}

		public void DeleteEdge(int edgeIndex) {
			_edgeWeights.Remove(edgeIndex);
			_graph.DeleteEdge(edgeIndex);
		}
		
		public WeightedNodeProxy<TNode, TEdge> CreateNodeProxy(TNode weight) {
			int index = CreateNode(weight);
			return new WeightedNodeProxy<TNode, TEdge>(this, index);
		}
		
		public WeightedEdgeProxy<TNode, TEdge> CreateEdgeProxy(int nodeFromIndex, int nodeToIndex, TEdge weight) {
			int edgeIndex = this.CreateEdge(nodeFromIndex, nodeToIndex, weight);
			return new WeightedEdgeProxy<TNode, TEdge>(this, edgeIndex);
		}

		public TNode GetNodeWeight(int nodeIndex) {
			return _nodeWeights[nodeIndex];
		}

		public TEdge GetEdgeWeight(int edgeIndex) {
			return _edgeWeights[edgeIndex];
		}

		public void SetNodeWeight(int nodeIndex, TNode weight) {
			_nodeWeights[nodeIndex] = weight;
		}

		public void SetEdgeWeight(int edgeIndex, TEdge weight) {
			_edgeWeights[edgeIndex] = weight;
		}

		public int FindEdge(int nodeFromIndex, int nodeToIndex) {
			return _graph.GetEdges(nodeFromIndex, nodeToIndex).First();
		}

		private Graph _graph;
		private Dictionary<int, TNode> _nodeWeights;
		private Dictionary<int, TEdge> _edgeWeights;
	}
}
