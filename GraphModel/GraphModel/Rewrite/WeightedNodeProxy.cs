﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class WeightedNodeProxy<TNode, TEdge> {

		public WeightedNodeProxy(WeightedGraph<TNode, TEdge> graph, int index) {
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

		public TNode Weight {
			get {
				return _graph.GetNodeWeight(_index);
			}
			set {
				_graph.SetNodeWeight(_index, value);
			}
		}

		public WeightedEdgeProxy<TNode, TEdge> AddOutgoingEdge(WeightedNodeProxy<TNode, TEdge> otherNode, TEdge weight) {
			return WeightedEdgeProxy<TNode, TEdge>.Create(_graph, _index, otherNode._index, weight);
		}
		
		public WeightedEdgeProxy<TNode, TEdge> AddIncomingEdge(WeightedNodeProxy<TNode, TEdge> otherNode, TEdge weight) {
			return WeightedEdgeProxy<TNode, TEdge>.Create(_graph, otherNode._index, _index, weight);
		}

		public void Delete() {
			_graph.DeleteNode(_index);
			_index = -1;
			_graph = null;
		}

		private WeightedGraph<TNode, TEdge> _graph;
		private int _index;
	}
}
