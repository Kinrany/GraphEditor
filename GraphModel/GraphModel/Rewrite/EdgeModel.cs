using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class EdgeModel : WeightedNodeProxy<GraphModel.NodeWeight, GraphModel.EdgeWeight> {
		public EdgeModel(GraphModel graph, int index) : base(graph, index) { }

		public new GraphModel Graph {
			get {
				return (GraphModel)base.Graph;
			}
		}

		public NodeModel NodeFrom {
			get {
				return new NodeModel(this.Graph, this.Graph.GetNodeFrom(this.Index));
			}
		}
		public NodeModel NodeTo {
			get {
				return new NodeModel(this.Graph, this.Graph.GetNodeTo(this.Index));
			}
		}
	}
}
