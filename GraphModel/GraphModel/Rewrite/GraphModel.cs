using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public partial class GraphModel : Graph {

		public GraphModel() {
			_nodeWeights = new Dictionary<NodeIndex, NodeWeight>();
			_edgeWeights = new Dictionary<EdgeIndex, EdgeWeight>();
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


		public new NodeIndex CreateNode() {
			return CreateNode(default(NodeWeight));
		}
		public NodeIndex CreateNode(NodeWeight weight) {
			NodeIndex nodeIndex = base.CreateNode();
			_nodeWeights[nodeIndex] = weight;

			FireChangedEvent();
			
			return nodeIndex;
		}

		public new EdgeIndex CreateEdge(NodeIndex nodeFromIndex, NodeIndex nodeToIndex) {
			return CreateEdge(nodeFromIndex, nodeToIndex, default(EdgeWeight));
		}
		public EdgeIndex CreateEdge(NodeIndex nodeFromIndex, NodeIndex nodeToIndex, EdgeWeight weight) {
			EdgeIndex edgeIndex = base.CreateEdge(nodeFromIndex, nodeToIndex);
			_edgeWeights[edgeIndex] = weight;

			FireChangedEvent();
			
			return edgeIndex;
		}

		public new void DeleteNode(NodeIndex nodeIndex) {
			NodeWeight weight = GetNodeWeight(nodeIndex);

			base.DeleteNode(nodeIndex);
			_nodeWeights.Remove(nodeIndex);

			FireChangedEvent();
		}

		public new void DeleteEdge(EdgeIndex edgeIndex) {
			EdgeWeight weight = GetEdgeWeight(edgeIndex);

			base.DeleteEdge(edgeIndex);
			_edgeWeights.Remove(edgeIndex);

			FireChangedEvent();
		}

		public NodeWeight GetNodeWeight(NodeIndex nodeIndex) {
			return _nodeWeights[nodeIndex];
		}

		public EdgeWeight GetEdgeWeight(EdgeIndex edgeIndex) {
			return _edgeWeights[edgeIndex];
		}

		public void SetNodeWeight(NodeIndex nodeIndex, NodeWeight weight) {
			NodeWeight oldWeight = GetNodeWeight(nodeIndex);

			_nodeWeights[nodeIndex] = weight;

			FireChangedEvent();
		}

		public void SetEdgeWeight(EdgeIndex edgeIndex, EdgeWeight weight) {
			EdgeWeight oldWeight = GetEdgeWeight(edgeIndex);

			_edgeWeights[edgeIndex] = weight;

			FireChangedEvent();
		}

		public void Reindex() {
			throw new NotImplementedException();
		}

		
		private Dictionary<NodeIndex, NodeWeight> _nodeWeights;
		private Dictionary<EdgeIndex, EdgeWeight> _edgeWeights;

		private string _text = "";
	}
}
