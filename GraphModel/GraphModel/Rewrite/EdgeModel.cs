using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		public string Value {
			get {
				return Weight.Value;
			}
			set {
				var weight = Weight;
				weight.Value = value;
				Weight = weight;
			}
		}

		public static IEnumerable<EdgeModel> Enumerate (GraphModel graph) {
			foreach (EdgeIndex edgeIndex in graph.EdgeEnumerator) {
				yield return new EdgeModel(graph, edgeIndex);
			}
		}

		public static EdgeModel Create(NodeModel nodeFrom, NodeModel nodeTo) {
			return Create(nodeFrom, nodeTo, new EdgeWeight());
		}
		public static EdgeModel Create(NodeModel nodeFrom, NodeModel nodeTo, EdgeWeight weight) {
			Debug.Assert(nodeFrom.Graph == nodeTo.Graph);
			var graph = nodeFrom.Graph;

			EdgeIndex edgeIndex = graph.CreateEdge(nodeFrom.Index, nodeTo.Index, weight);
			return new EdgeModel(graph, edgeIndex);
		}

		public static EdgeModel Between(NodeModel nodeFrom, NodeModel nodeTo) {
			Debug.Assert(nodeFrom.Graph == nodeTo.Graph);
			GraphModel graph = nodeFrom.Graph;

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
