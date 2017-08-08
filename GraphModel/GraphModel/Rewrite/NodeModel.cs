using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	using NodeWeight = GraphModel.NodeWeight;
	using EdgeWeight = GraphModel.EdgeWeight;

	public class NodeModel{
		public NodeModel(GraphModel graph, NodeIndex index) {
			_graph = graph;
			_index = index;
		}

		public GraphModel Graph {
			get {
				return _graph;
			}
		}

		public NodeIndex Index {
			get {
				ThrowUnlessValid();
				return _index;
			}
		}

		public bool IsValid {
			get {
				return _graph != null && _graph.ContainsNode(_index);
			}
		}

		public NodeWeight Weight {
			get {
				ThrowUnlessValid();
				return _graph.GetNodeWeight(_index);
			}
			set {
				ThrowUnlessValid();
				_graph.SetNodeWeight(_index, value);
			}
		}

		public Color Color {
			get {
				return Weight.Color;
			}
			set {
				var weight = Weight;
				weight.Color = value;
				Weight = weight;
			}
		}

		public Point Location {
			get {
				return Weight.Location;
			}
			set {
				var weight = Weight;
				weight.Location = value;
				Weight = weight;
			}
		}

		public static bool operator ==(NodeModel node1, NodeModel node2) {
			if (object.ReferenceEquals(node1, node2)) {
				return true;
			}
			if (object.ReferenceEquals(node1, null) || object.ReferenceEquals(node2, null)) {
				return false;
			}

			return node1._graph == node2._graph && node1._index == node2._index;
		}
		public static bool operator !=(NodeModel node1, NodeModel node2) {
			return !(node1 == node2);
		}
		public override bool Equals(object obj) {
			NodeModel other = obj as NodeModel;
			return other != null && this == other;
		}
		public override int GetHashCode() {
			int hash = 486187739;
			hash = (hash + 16777619) ^ _graph.GetHashCode();
			hash = (hash + 16777619) ^ _index.GetHashCode();
			return hash;
		}

		public static NodeModel Create(GraphModel graph, NodeWeight weight) {
			NodeIndex nodeIndex = graph.CreateNode(weight);
			return new NodeModel(graph, nodeIndex);
		}

		public EdgeModel AddOutgoingEdge(NodeModel otherNode, EdgeWeight weight = null) {
			if (weight == null) {
				weight = new EdgeWeight();
			}

			EdgeIndex edgeIndex = _graph.CreateEdge(_index, otherNode.Index, weight);
			return new EdgeModel(_graph, edgeIndex);
		}

		public EdgeModel AddIncomingEdge(NodeModel otherNode, EdgeWeight weight = null) {
			if (weight == null) {
				weight = new EdgeWeight();
			}

			EdgeIndex edgeIndex = _graph.CreateEdge(otherNode.Index, _index, weight);
			return new EdgeModel(_graph, edgeIndex);
		}

		public void Delete() {
			ThrowUnlessValid();
			_graph.DeleteNode(_index);
			_index = NodeIndex.NaN;
			_graph = null;
		}

		public IEnumerable<EdgeModel> OutgoingEnumerator {
			get {
				foreach (EdgeIndex edgeIndex in _graph.OutgoingEnumerator(_index)) {
					yield return new EdgeModel(_graph, edgeIndex);
				}
			}
		}

		public IEnumerable<EdgeModel> IncomingEnumerator {
			get {
				foreach (EdgeIndex edgeIndex in _graph.IncomingEnumerator(_index)) {
					yield return new EdgeModel(_graph, edgeIndex);
				}
			}
		}

		public static IEnumerable<NodeModel> Enumerate(GraphModel graph) {
			foreach (NodeIndex nodeIndex in graph.NodeEnumerator) {
				yield return new NodeModel(graph, nodeIndex);
			}
		}


		private GraphModel _graph;
		private NodeIndex _index;

		private void ThrowUnlessValid() {
			if (!this.IsValid) {
				throw new InvalidOperationException("This is not a valid node object.");
			}
		}
	}
}
