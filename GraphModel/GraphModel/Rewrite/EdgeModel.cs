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
