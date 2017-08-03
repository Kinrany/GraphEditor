using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	using NodeWeight = GraphModel.NodeWeight;
	using EdgeWeight = GraphModel.EdgeWeight;

	public class EdgeModel {
		public EdgeModel(GraphModel graph, EdgeIndex index) {
			_graph = graph;
			_index = index;
		}

		public GraphModel Graph {
			get {
				return _graph;
			}
		}

		public EdgeIndex Index {
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

		public NodeModel NodeFrom {
			get {
				return new NodeModel(_graph, _graph.GetNodeFrom(_index));
			}
		}
		public NodeModel NodeTo {
			get {
				return new NodeModel(_graph, _graph.GetNodeTo(_index));
			}
		}

		public EdgeWeight Weight {
			get {
				return _graph.GetEdgeWeight(_index);
			}
			set {
				_graph.SetEdgeWeight(_index, value);
			}
		}

		public static IEnumerable<EdgeModel> Enumerate (GraphModel graph) {
			foreach (EdgeIndex edgeIndex in graph.EdgeEnumerator) {
				yield return new EdgeModel(graph, edgeIndex);
			}
		}

		public static EdgeModel Create(GraphModel graph, NodeIndex nodeFromIndex, NodeIndex nodeToIndex, EdgeWeight weight = null) {
			if (weight == null) {
				weight = new EdgeWeight();
			}

			EdgeIndex edgeIndex = graph.CreateEdge(nodeFromIndex, nodeToIndex, weight);
			return new EdgeModel(graph, edgeIndex);
		}

		public static EdgeModel Between(GraphModel graph, NodeModel nodeFrom, NodeModel nodeTo) {
			EdgeIndex? edgeIndex = graph.GetEdgeBetween(nodeFrom.Index, nodeTo.Index);
			if (edgeIndex == null) {
				return null;
			}
			else {
				return new EdgeModel(graph, (EdgeIndex)edgeIndex);
			}
		}

		public void Delete() {
			_graph.DeleteEdge(_index);
			_index = EdgeIndex.NaN;
			_graph = null;
		}


		private GraphModel _graph;
		private EdgeIndex _index;

		private void ThrowUnlessValid() {
			if (!this.IsValid) {
				throw new InvalidOperationException("This is not a valid node object.");
			}
		}
	}
}
