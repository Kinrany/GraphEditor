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

		public int Index {
			get {
				if (IsValid) {
					return _index;
				}
				else {
					throw new InvalidOperationException("This is not a valid node object.");
				}
			}
		}

		public bool IsValid {
			get {
				return _graph != null && _graph.ContainsNode(_index);
			}
		}

		public EdgeProxy AddOutgoingEdge(NodeProxy otherNode) {
			return EdgeProxy.Create(_graph, _index, otherNode._index);
		}

		public EdgeProxy AddIncomingEdge(NodeProxy otherNode) {
			return EdgeProxy.Create(_graph, otherNode._index, _index);
		}

		public void Delete() {
			_graph.DeleteNode(_index);
			_index = -1;
			_graph = null;
		}

		private Graph _graph;
		private int _index;
	}
}
