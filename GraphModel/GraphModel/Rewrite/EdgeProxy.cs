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

		public bool IsValid {
			get {
				return _graph != null && _graph.ContainsEdge(_index);
			}
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
