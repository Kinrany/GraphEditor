using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class WeightedNodeProxy<TN, TE> where TN:new() where TE : new() {

		public WeightedNodeProxy(WeightedGraph<TN, TE> graph, int index) {
			this._graph = graph;
			this._index = index;
		}

		public WeightedEdgeProxy<TN, TE> AddOutgoingEdge(WeightedNodeProxy<TN, TE> otherNode) {
			return _graph.CreateEdgeProxy(_index, otherNode._index);
		}
		public WeightedEdgeProxy<TN, TE> AddOutgoingEdge(WeightedNodeProxy<TN, TE> otherNode, TE weight) {
			return _graph.CreateEdgeProxy(_index, otherNode._index, weight);
		}

		public WeightedEdgeProxy<TN, TE> AddIncomingEdge(WeightedNodeProxy<TN, TE> otherNode) {
			return _graph.CreateEdgeProxy(otherNode._index, _index);
		}
		public WeightedEdgeProxy<TN, TE> AddIncomingEdge(WeightedNodeProxy<TN, TE> otherNode, TE weight) {
			return _graph.CreateEdgeProxy(otherNode._index, _index, weight);
		}

		public void Delete() {
			_graph.DeleteNode(_index);
			_index = -1;
			_graph = null;
		}

		private WeightedGraph<TN, TE> _graph;
		private int _index;
	}
}
