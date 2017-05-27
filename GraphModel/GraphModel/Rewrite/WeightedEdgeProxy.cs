using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class WeightedEdgeProxy<TNode, TEdge> {

		public WeightedEdgeProxy(WeightedGraph<TNode, TEdge> graph, int index) {
			this._graph = graph;
			this._index = index;
		}

		public void Delete() {
			_graph.DeleteEdge(_index);
			_index = -1;
			_graph = null;
		}

		private WeightedGraph<TNode, TEdge> _graph;
		private int _index;
	}
}
