using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	using NodeWeight = GraphModel.NodeWeight;
	using EdgeWeight = GraphModel.EdgeWeight;

	public class EdgeModel : WeightedEdgeProxy<NodeWeight, EdgeWeight> {
		public EdgeModel(GraphModel graph, EdgeIndex index) : base(graph, index) { }

		public new GraphModel Graph {
			get {
				return (GraphModel)base.Graph;
			}
		}

		public new NodeModel NodeFrom {
			get {
				return new NodeModel(this.Graph, this.Graph.GetNodeFrom(this.Index));
			}
		}
		public new NodeModel NodeTo {
			get {
				return new NodeModel(this.Graph, this.Graph.GetNodeTo(this.Index));
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
	}
}
