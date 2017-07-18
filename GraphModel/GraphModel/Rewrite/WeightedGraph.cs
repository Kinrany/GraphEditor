using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class WeightedGraph<TNode, TEdge> {

		public WeightedGraph() {
			_nodeWeights = new Dictionary<NodeIndex, TNode>();
			_edgeWeights = new Dictionary<EdgeIndex, TEdge>();
			_graph = new Graph();
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

		public NodeIndex CreateNode(TNode weight) {
			NodeIndex index = _graph.CreateNode();
			_nodeWeights[index] = weight;

			this.ChangedEvent();

			return index;
		}
		
		public EdgeIndex CreateEdge(NodeIndex nodeFromIndex, NodeIndex nodeToIndex, TEdge weight) {
			EdgeIndex edgeIndex = _graph.CreateEdge(nodeFromIndex, nodeToIndex);
			_edgeWeights[edgeIndex] = weight;

			this.ChangedEvent();

			return edgeIndex;
		}

		public void DeleteNode(NodeIndex nodeIndex) {
			_graph.DeleteNode(nodeIndex);
			_nodeWeights.Remove(nodeIndex);

			this.ChangedEvent();
		}

		public void DeleteEdge(EdgeIndex edgeIndex) {
			_graph.DeleteEdge(edgeIndex);
			_edgeWeights.Remove(edgeIndex);

			this.ChangedEvent();
		}

		public NodeIndex GetNodeFrom(EdgeIndex edgeIndex) {
			return _graph.GetNodeFrom(edgeIndex);
		}

		public NodeIndex GetNodeTo(EdgeIndex edgeIndex) {
			return _graph.GetNodeTo(edgeIndex);
		}

		public TNode GetNodeWeight(NodeIndex nodeIndex) {
			return _nodeWeights[nodeIndex];
		}

		public TEdge GetEdgeWeight(EdgeIndex edgeIndex) {
			return _edgeWeights[edgeIndex];
		}

		public void SetNodeWeight(NodeIndex nodeIndex, TNode weight) {
			_nodeWeights[nodeIndex] = weight;

			this.ChangedEvent();
		}

		public void SetEdgeWeight(EdgeIndex edgeIndex, TEdge weight) {
			_edgeWeights[edgeIndex] = weight;

			this.ChangedEvent();
		}

		public EdgeIndex? GetEdgeBetween(NodeIndex nodeFromIndex, NodeIndex nodeToIndex) {
			EdgeIndex[] indices = _graph.GetEdgesBetween(nodeFromIndex, nodeToIndex);
			if (indices.Length > 0) {
				return indices.First();
			}
			else {
				return null;
			}
		}

		public bool ContainsNode(NodeIndex nodeIndex) {
			return _graph.ContainsNode(nodeIndex);
		}

		public bool ContainsEdge(EdgeIndex edgeIndex) {
			return _graph.ContainsEdge(edgeIndex);
		}

		public IEnumerable<NodeIndex> NodeEnumerator {
			get {
				return _graph.NodeEnumerator;
			}
		}

		public IEnumerable<EdgeIndex> EdgeEnumerator {
			get {
				return _graph.EdgeEnumerator;
			}
		}

		public IEnumerable<EdgeIndex> OutgoingEnumerator(NodeIndex nodeIndex) {
			return _graph.OutgoingEnumerator(nodeIndex);
		}

		public IEnumerable<EdgeIndex> IncomingEnumerator(NodeIndex nodeIndex) {
			return _graph.IncomingEnumerator(nodeIndex);
		}
		
		public event Action ChangedEvent = () => { };


		private Graph _graph;
		private Dictionary<NodeIndex, TNode> _nodeWeights;
		private Dictionary<EdgeIndex, TEdge> _edgeWeights;
	}
}
