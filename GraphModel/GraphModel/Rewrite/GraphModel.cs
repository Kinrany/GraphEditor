using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public partial class GraphModel {

		public GraphModel() {
			_nodeWeights = new Dictionary<NodeIndex, NodeWeight>();
			_edgeWeights = new Dictionary<EdgeIndex, EdgeWeight>();
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

		public string Text {
			get {
				return _text;
			}
			set {
				if (value == null) {
					throw new InvalidOperationException("Text can't be null.");
				}

				_text = value;
			}
		}

		public event Action ChangedEvent = () => { };

		public NodeIndex CreateNode(NodeWeight weight) {
			NodeIndex nodeIndex = _graph.CreateNode();
			_nodeWeights[nodeIndex] = weight;

			ChangedEvent();

			this.ChangedEvent += weight.FireChangedEvent;
			return nodeIndex;
		}

		public EdgeIndex CreateEdge(NodeIndex nodeFromIndex, NodeIndex nodeToIndex, EdgeWeight weight) {
			EdgeIndex edgeIndex = _graph.CreateEdge(nodeFromIndex, nodeToIndex);
			_edgeWeights[edgeIndex] = weight;

			ChangedEvent();
			
			this.ChangedEvent += weight.FireChangedEvent;
			return edgeIndex;
		}

		public void DeleteNode(NodeIndex nodeIndex) {
			NodeWeight weight = this.GetNodeWeight(nodeIndex);

			_graph.DeleteNode(nodeIndex);
			_nodeWeights.Remove(nodeIndex);

			this.ChangedEvent();
			
			this.ChangedEvent -= weight.FireChangedEvent;
		}

		public void DeleteEdge(EdgeIndex edgeIndex) {
			EdgeWeight weight = this.GetEdgeWeight(edgeIndex);

			_graph.DeleteEdge(edgeIndex);
			_edgeWeights.Remove(edgeIndex);

			this.ChangedEvent();

			this.ChangedEvent -= weight.FireChangedEvent;
		}

		public NodeIndex GetNodeFrom(EdgeIndex edgeIndex) {
			return _graph.GetNodeFrom(edgeIndex);
		}

		public NodeIndex GetNodeTo(EdgeIndex edgeIndex) {
			return _graph.GetNodeTo(edgeIndex);
		}

		public bool ContainsNode(NodeIndex nodeIndex) {
			return _graph.ContainsNode(nodeIndex);
		}

		public bool ContainsEdge(EdgeIndex edgeIndex) {
			return _graph.ContainsEdge(edgeIndex);
		}

		public NodeWeight GetNodeWeight(NodeIndex nodeIndex) {
			return _nodeWeights[nodeIndex];
		}

		public EdgeWeight GetEdgeWeight(EdgeIndex edgeIndex) {
			return _edgeWeights[edgeIndex];
		}

		public void SetNodeWeight(NodeIndex nodeIndex, NodeWeight weight) {
			NodeWeight oldWeight = this.GetNodeWeight(nodeIndex);
			this.ChangedEvent -= oldWeight.FireChangedEvent;
			this.ChangedEvent += weight.FireChangedEvent;

			_nodeWeights[nodeIndex] = weight;

			this.ChangedEvent();
		}

		public void SetEdgeWeight(EdgeIndex edgeIndex, EdgeWeight weight) {
			EdgeWeight oldWeight = this.GetEdgeWeight(edgeIndex);
			this.ChangedEvent -= oldWeight.FireChangedEvent;
			this.ChangedEvent += weight.FireChangedEvent;

			_edgeWeights[edgeIndex] = weight;

			this.ChangedEvent();
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

		public void Reindex() {
			throw new NotImplementedException();
		}


		private Graph _graph;
		private Dictionary<NodeIndex, NodeWeight> _nodeWeights;
		private Dictionary<EdgeIndex, EdgeWeight> _edgeWeights;

		private string _text = "";
	}
}
