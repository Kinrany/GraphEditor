using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class WeightedGraph<TNode, TEdge> {

		public WeightedGraph() {
			_nodeWeights = new Dictionary<int, TNode>();
			_edgeWeights = new Dictionary<int, TEdge>();
			_graph = new Graph();

			_graph.NodeReindexEvent += (int oldIndex, int newIndex) => {
				this.NodeReindexEvent(oldIndex, newIndex);
			};
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

			this.ChangedEvent();

			return index;
		}
		
		public int CreateEdge(int nodeFromIndex, int nodeToIndex, TEdge weight) {
			int edgeIndex = _graph.CreateEdge(nodeFromIndex, nodeToIndex);
			_edgeWeights[edgeIndex] = weight;

			this.ChangedEvent();

			return edgeIndex;
		}

		public void DeleteNode(int nodeIndex) {
			_graph.DeleteNode(nodeIndex);
			_nodeWeights.Remove(nodeIndex);

			this.ChangedEvent();
		}

		public void DeleteEdge(int edgeIndex) {
			_graph.DeleteEdge(edgeIndex);
			_edgeWeights.Remove(edgeIndex);

			this.ChangedEvent();
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

			this.ChangedEvent();
		}

		public void SetEdgeWeight(int edgeIndex, TEdge weight) {
			_edgeWeights[edgeIndex] = weight;

			this.ChangedEvent();
		}

		public int? GetEdgeBetween(int nodeFromIndex, int nodeToIndex) {
			int[] indices = _graph.GetEdgesBetween(nodeFromIndex, nodeToIndex);
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

		public IEnumerable<int> OutgoingEnumerator(int nodeIndex) {
			return _graph.OutgoingEnumerator(nodeIndex);
		}

		public IEnumerable<int> IncomingEnumerator(int nodeIndex) {
			return _graph.IncomingEnumerator(nodeIndex);
		}

		public void Reindex() {
			_graph.Reindex();
		}

		public event Action<int, int> NodeReindexEvent = (int _, int __) => { };

		public event Action ChangedEvent = () => { };


		private Graph _graph;
		private Dictionary<int, TNode> _nodeWeights;
		private Dictionary<int, TEdge> _edgeWeights;
	}
}
