using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class EdgeProxy {
		public EdgeProxy(Graph graph, EdgeIndex index) {
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
				ThrowUnlessValid();
				return new NodeProxy(_graph, _graph.GetNodeFrom(_index));
			}
		}
		public NodeProxy NodeTo {
			get {
				ThrowUnlessValid();
				return new NodeProxy(_graph, _graph.GetNodeTo(_index));
			}
		}

		public static bool operator ==(EdgeProxy edge1, EdgeProxy edge2) {
			if (object.ReferenceEquals(edge1, edge2)) {
				return true;
			}
			if (object.ReferenceEquals(edge1, null) || object.ReferenceEquals(edge2, null)) {
				return false;
			}

			return edge1._graph == edge2._graph && edge1._index == edge2._index;
		}
		public static bool operator !=(EdgeProxy edge1, EdgeProxy edge2) {
			return !(edge1 == edge2);
		}
		public override bool Equals(object obj) {
			EdgeProxy other = obj as EdgeProxy;
			return other != null && this == other;
		}
		public override int GetHashCode() {
			int hash = 486187739;
			hash = (hash + 16777619) ^ _graph.GetHashCode();
			hash = (hash + 16777619) ^ _index.GetHashCode();
			return hash;
		}

		public static EdgeProxy Create(Graph graph, NodeIndex nodeToIndex, NodeIndex nodeFromIndex) {
			EdgeIndex edgeIndex = graph.CreateEdge(nodeToIndex, nodeFromIndex);
			return new EdgeProxy(graph, edgeIndex);
		}
		public static EdgeProxy Create(Graph graph, NodeProxy nodeTo, NodeProxy nodeFrom) {
			return Create(graph, nodeTo.Index, nodeFrom.Index);
		}

		public void Delete() {
			ThrowUnlessValid();
			_graph.DeleteEdge(_index);
			_index = EdgeIndex.NaN;
			_graph = null;
		}

		private Graph _graph;
		private EdgeIndex _index;

		private void ThrowUnlessValid() {
			if (!this.IsValid) {
				throw new InvalidOperationException("This is not a valid node object.");
			}
		}
	}
}
