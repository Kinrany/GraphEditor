using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class EdgeModel : WeightedEdgeProxy<GraphModel.NodeWeight, GraphModel.EdgeWeight> {
		public EdgeModel(GraphModel graph, int index) : base(graph, index) { }

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
			foreach (int edgeIndex in graph.EdgeEnumerator) {
				yield return new EdgeModel(graph, edgeIndex);
			}
		}

		public static EdgeModel Create(GraphModel graph, int nodeFromIndex, int nodeToIndex, GraphModel.EdgeWeight weight = null) {
			if (weight == null) {
				weight = new GraphModel.EdgeWeight();
			}

			int edgeIndex = graph.CreateEdge(nodeFromIndex, nodeToIndex, weight);
			return new EdgeModel(graph, edgeIndex);
		}
		public static EdgeModel Create(GraphModel graph, EdgeProxy nodeFrom, EdgeProxy nodeTo, GraphModel.EdgeWeight weight = null) {
			return Create(graph, nodeFrom.Index, nodeTo.Index, weight);
		}

		public static EdgeModel Between(GraphModel graph, NodeModel nodeFrom, NodeModel nodeTo) {
			int? edgeIndex = graph.GetEdgeBetween(nodeFrom.Index, nodeTo.Index);
			if (edgeIndex == null) {
				return null;
			}
			else {
				return new EdgeModel(graph, (int)edgeIndex);
			}
		}
	}
}
