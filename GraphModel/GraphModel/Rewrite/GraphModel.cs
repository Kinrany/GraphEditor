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

		public event Action NodeWeightChanged;
		public event Action EdgeWeightChanged;

		public NodeIndex CreateNode(NodeWeight weight) {
			NodeIndex nodeIndex = base.CreateNode();
			SetNodeWeight(nodeIndex, weight);
			return nodeIndex;
		}
		
		public EdgeIndex CreateEdge(NodeIndex nodeFromIndex, NodeIndex nodeToIndex, EdgeWeight weight) {
			EdgeIndex edgeIndex = base.CreateEdge(nodeFromIndex, nodeToIndex);
			SetEdgeWeight(edgeIndex, weight);
			return edgeIndex;
		}

		public NodeWeight GetNodeWeight(NodeIndex nodeIndex) {
			if (!ContainsNode(nodeIndex)) {
				throw new ArgumentException("Invalid node index.");
			}

			NodeWeight weight;
			if (_nodeWeights.TryGetValue(nodeIndex, out weight)) {
				return weight;
			}
			else {
				return default(NodeWeight);
			}
		}

		public EdgeWeight GetEdgeWeight(EdgeIndex edgeIndex) {
			if (!ContainsEdge(edgeIndex)) {
				throw new ArgumentException("Invalid edge index.");
			}

			EdgeWeight weight;
			if (_edgeWeights.TryGetValue(edgeIndex, out weight)) {
				return weight;
			}
			else {
				return default(EdgeWeight);
			}
		}

		public void SetNodeWeight(NodeIndex nodeIndex, NodeWeight weight) {
			NodeWeight oldWeight = GetNodeWeight(nodeIndex);

			_nodeWeights[nodeIndex] = weight;

			FireNodeWeightChanged();
		}

		public void SetEdgeWeight(EdgeIndex edgeIndex, EdgeWeight weight) {
			EdgeWeight oldWeight = GetEdgeWeight(edgeIndex);

			_edgeWeights[edgeIndex] = weight;

			FireEdgeWeightChanged();
		}

		public void Reindex() {
			throw new NotImplementedException();
		}

		
		private Dictionary<NodeIndex, NodeWeight> _nodeWeights;
		private Dictionary<EdgeIndex, EdgeWeight> _edgeWeights;

		private string _text = "";

		private void FireNodeWeightChanged() {
			NodeWeightChanged?.Invoke();
			FireGraphChanged();
		}
		private void FireEdgeWeightChanged() {
			EdgeWeightChanged?.Invoke();
			FireGraphChanged();
		}
	}
}
