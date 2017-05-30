using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class WeightedNodeProxy<TNode, TEdge> {

		public WeightedNodeProxy(WeightedGraph<TNode, TEdge> graph, int index) {
			this._graph = graph;
			this._index = index;
		}

		public WeightedGraph<TNode, TEdge> Graph {
			get {
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

		public static bool operator ==(WeightedNodeProxy<TNode, TEdge> node1, WeightedNodeProxy<TNode, TEdge> node2) {
			if (object.ReferenceEquals(node1, node2)) {
				return true;
			}
			if (object.ReferenceEquals(node1, null) || object.ReferenceEquals(node2, null)) {
				return false;
			}

			return node1._graph == node2._graph && node1._index == node2._index;
		}
		public static bool operator !=(WeightedNodeProxy<TNode, TEdge> node1, WeightedNodeProxy<TNode, TEdge> node2) {
			return !(node1 == node2);
		}
		public override bool Equals(object obj) {
			WeightedNodeProxy<TNode, TEdge> other = obj as WeightedNodeProxy<TNode, TEdge>;
			return other != null && this == other;
		}
		public override int GetHashCode() {
			int hash = 486187739;
			hash = (hash + 16777619) ^ _graph.GetHashCode();
			hash = (hash + 16777619) ^ _index.GetHashCode();
			return hash;
		}

		public WeightedEdgeProxy<TNode, TEdge> AddOutgoingEdge(WeightedNodeProxy<TNode, TEdge> otherNode, TEdge weight) {
			return WeightedEdgeProxy<TNode, TEdge>.Create(_graph, _index, otherNode._index, weight);
		}
		
		public WeightedEdgeProxy<TNode, TEdge> AddIncomingEdge(WeightedNodeProxy<TNode, TEdge> otherNode, TEdge weight) {
			return WeightedEdgeProxy<TNode, TEdge>.Create(_graph, otherNode._index, _index, weight);
		}

		public IEnumerable<WeightedEdgeProxy<TNode, TEdge>> OutgoingEnumerator {
			get {
				foreach (int edgeIndex in _graph.OutgoingEnumerator(_index)) {
					yield return new WeightedEdgeProxy<TNode, TEdge>(_graph, edgeIndex);
				}
			}
		}

		public IEnumerable<WeightedEdgeProxy<TNode, TEdge>> IncomingEnumerator {
			get {
				foreach (int edgeIndex in _graph.IncomingEnumerator(_index)) {
					yield return new WeightedEdgeProxy<TNode, TEdge>(_graph, edgeIndex);
				}
			}
		}

		public static IEnumerable<WeightedNodeProxy<TNode, TEdge>> Enumerate(WeightedGraph<TNode, TEdge> graph) {
			foreach (int nodeIndex in graph.NodeEnumerator) {
				yield return new WeightedNodeProxy<TNode, TEdge>(graph, nodeIndex);
			}
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
