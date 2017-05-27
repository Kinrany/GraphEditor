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
				return _graph != null && _graph.ContainsEdge(_index);
			}
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
	}
}
