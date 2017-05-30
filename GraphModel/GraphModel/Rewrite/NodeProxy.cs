using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class NodeProxy {
		public NodeProxy(Graph graph, int index) {
			this._graph = graph;
			this._index = index;

			_graph.NodeReindexEvent += NodeReindexHandler;
		}

		public Graph Graph {
			get {
				ThrowUnlessValid();
				return _graph;
			}
		}

		public int Index {
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

		public static bool operator==(NodeProxy node1, NodeProxy node2) {
			if (object.ReferenceEquals(node1, node2)) {
				return true;
			}
			if (object.ReferenceEquals(node1, null) || object.ReferenceEquals(node2, null)) {
				return false;
			}

			return node1._graph == node2._graph && node1._index == node2._index;
		}
		public static bool operator!=(NodeProxy node1, NodeProxy node2) {
			return !(node1 == node2);
		}
		public override bool Equals(object obj) {
			NodeProxy other = obj as NodeProxy;
			return other != null && this == other;
		}
		public override int GetHashCode() {
			int hash = 486187739;
			hash = (hash + 16777619) ^ _graph.GetHashCode();
			hash = (hash + 16777619) ^ _index.GetHashCode();
			return hash;
		}

		public EdgeProxy AddOutgoingEdge(NodeProxy otherNode) {
			return EdgeProxy.Create(_graph, _index, otherNode._index);
		}

		public EdgeProxy AddIncomingEdge(NodeProxy otherNode) {
			return EdgeProxy.Create(_graph, otherNode._index, _index);
		}

		public IEnumerable<EdgeProxy> OutgoingEnumerator {
			get {
				ThrowUnlessValid();
				foreach (int edgeIndex in _graph.OutgoingEnumerator(_index)) {
					yield return new EdgeProxy(_graph, edgeIndex);
				}
			}
		}

		public IEnumerable<EdgeProxy> IncomingEnumerator {
			get {
				ThrowUnlessValid();
				foreach (int edgeIndex in _graph.IncomingEnumerator(_index)) {
					yield return new EdgeProxy(_graph, edgeIndex);
				}
			}
		}

		public static IEnumerable<NodeProxy> Enumerate(Graph graph) {
			foreach (int nodeIndex in graph.NodeEnumerator) {
				yield return new NodeProxy(graph, nodeIndex);
			}
		}

		public void Delete() {
			ThrowUnlessValid();
			_graph.DeleteNode(_index);
			_graph.NodeReindexEvent -= NodeReindexHandler;
			_index = -1;
			_graph = null;
		}


		private Graph _graph;
		private int _index;

		private void ThrowUnlessValid() {
			if (!this.IsValid) {
				throw new InvalidOperationException("This is not a valid node object.");
			}
		}

		private void NodeReindexHandler(int oldIndex, int newIndex) {
			if (_index == oldIndex) {
				_index = newIndex;
			}
		}
	}
}
