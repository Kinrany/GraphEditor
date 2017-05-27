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
		
		public int NodeCount {
			get {
				return _graph.NodeCount;
			}
		}

		public int EdgeCount {
			get {
				return _graph.EdgeCount;
			}
		}

		/// <summary>
		/// Index of the last node in this graph.
		/// </summary>
		public int LastNode {
			get {
				return _graph.LastNode;
			}
		}

		/// <summary>
		/// Index of the last edge in this graph.
		/// </summary>
		public int LastEdge {
			get {
				return _graph.LastEdge;
			}
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

		public int GetNodeFrom(int edgeIndex) {
			return _graph.GetNodeFrom(edgeIndex);
		}

		public int GetNodeTo(int edgeIndex) {
			return _graph.GetNodeTo(edgeIndex);
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

		public int? GetEdge(int nodeFromIndex, int nodeToIndex) {
			int[] indices = _graph.GetEdges(nodeFromIndex, nodeToIndex);
			if (indices.Length > 0) {
				return indices.First();
			}
			else {
				return null;
			}
		}

		public bool ContainsNode(int nodeIndex) {
			return _graph.ContainsNode(nodeIndex);
		}

		public bool ContainsEdge(int edgeIndex) {
			return _graph.ContainsEdge(edgeIndex);
		}

		public IEnumerable<int> NodeEnumerator {
			get {
				return _graph.NodeEnumerator;
			}
		}

		public IEnumerable<int> EdgeEnumerator {
			get {
				return _graph.EdgeEnumerator;
			}
		}

		public void Reindex() {
			_graph.NodeReindexEvent += CallNodeReindexEvent;
			_graph.EdgeReindexEvent += CallEdgeReindexEvent;
			_graph.Reindex();
			_graph.NodeReindexEvent -= CallNodeReindexEvent;
			_graph.EdgeReindexEvent -= CallEdgeReindexEvent;
		}

		public event Action<int, int> NodeReindexEvent;
		public event Action<int, int> EdgeReindexEvent;


		private Graph _graph;
		private Dictionary<int, TNode> _nodeWeights;
		private Dictionary<int, TEdge> _edgeWeights;

		private void CallNodeReindexEvent(int arg1, int arg2) {
			NodeReindexEvent(arg1, arg2);
		}

		private void CallEdgeReindexEvent(int arg1, int arg2) {
			EdgeReindexEvent(arg1, arg2);
		}
	}
}
