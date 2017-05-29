using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class NodeProxy {
		public NodeProxy(Graph graph, int index) {
			this._graph = graph;
			this._index = index;
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
			return node1._graph == node2._graph && node1._index == node2._index;
		}
		public static bool operator!=(NodeProxy node1, NodeProxy node2) {
			return !(node1 == node2);
		}

		public EdgeProxy AddOutgoingEdge(NodeProxy otherNode) {
			return EdgeProxy.Create(_graph, _index, otherNode._index);
		}

		public EdgeProxy AddIncomingEdge(NodeProxy otherNode) {
			return EdgeProxy.Create(_graph, otherNode._index, _index);
		}

		public IEnumerable<EdgeProxy> OutgoingEnumerator {
			get {
				foreach (int edgeIndex in _graph.OutgoingEnumerator(_index)) {
					yield return new EdgeProxy(_graph, edgeIndex);
				}
			}
		}

		public IEnumerable<EdgeProxy> IncomingEnumerator {
			get {
				foreach (int edgeIndex in _graph.IncomingEnumerator(_index)) {
					yield return new EdgeProxy(_graph, edgeIndex);
				}
			}
		}

		public void Delete() {
			_graph.DeleteNode(_index);
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
	}
}
