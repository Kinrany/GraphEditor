using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class EdgeProxy {
		public EdgeProxy(Graph graph, int index) {
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
				return _graph != null && _graph.ContainsEdge(_index);
			}
		}

		public NodeProxy NodeFrom {
			get {
				return new NodeProxy(_graph, _graph.GetNodeFrom(_index));
			}
		}
		public NodeProxy NodeTo {
			get {
				return new NodeProxy(_graph, _graph.GetNodeTo(_index));
			}
		}

		public static bool operator ==(EdgeProxy edge1, EdgeProxy edge2) {
			return edge1._graph == edge2._graph && edge1._index == edge2._index;
		}
		public static bool operator !=(EdgeProxy edge1, EdgeProxy edge2) {
			return !(edge1 == edge2);
		}

		public static EdgeProxy Create(Graph graph, int nodeToIndex, int nodeFromIndex) {
			int index = graph.CreateEdge(nodeToIndex, nodeFromIndex);
			return new EdgeProxy(graph, index);
		}
		public static EdgeProxy Create(Graph graph, NodeProxy nodeTo, NodeProxy nodeFrom) {
			return Create(graph, nodeTo.Index, nodeFrom.Index);
		}

		public void Delete() {
			_graph.DeleteEdge(_index);
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
