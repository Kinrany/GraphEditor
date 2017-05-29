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

		public WeightedGraph<TNode, TEdge> Graph {
			get {
				ThrowUnlessValid();
				return _graph;
			}
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

		public TEdge Weight {
			get {
				return _graph.GetEdgeWeight(_index);
			}
			set {
				_graph.SetEdgeWeight(_index, value);
			}
		}

		public WeightedNodeProxy<TNode, TEdge> NodeFrom {
			get {
				return new WeightedNodeProxy<TNode, TEdge>(_graph, _graph.GetNodeFrom(_index));
			}
		}
		public WeightedNodeProxy<TNode, TEdge> NodeTo {
			get {
				return new WeightedNodeProxy<TNode, TEdge>(_graph, _graph.GetNodeTo(_index));
			}
		}

		public static WeightedEdgeProxy<TNode, TEdge> Create(WeightedGraph<TNode, TEdge> graph, int nodeFromIndex, int nodeToIndex, TEdge weight) {
			int edgeIndex = graph.CreateEdge(nodeFromIndex, nodeToIndex, weight);
			return new WeightedEdgeProxy<TNode, TEdge>(graph, edgeIndex);
		}
		public static WeightedEdgeProxy<TNode, TEdge> Create(WeightedGraph<TNode, TEdge> graph, WeightedEdgeProxy<TNode, TEdge> nodeFrom, WeightedEdgeProxy<TNode, TEdge> nodeTo, TEdge weight) {
			return Create(graph, nodeFrom.Index, nodeTo.Index, weight);
		}

		public void Delete() {
			_graph.DeleteEdge(_index);
			_index = -1;
			_graph = null;
		}

		private WeightedGraph<TNode, TEdge> _graph;
		private int _index;

		private void ThrowUnlessValid() {
			if (!this.IsValid) {
				throw new InvalidOperationException("This is not a valid node object.");
			}
		}
	}
}
