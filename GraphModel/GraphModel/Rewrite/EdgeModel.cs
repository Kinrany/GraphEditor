using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class EdgeModel : WeightedNodeProxy<GraphModel.NodeWeight, GraphModel.EdgeWeight> {
		public EdgeModel(GraphModel graph, int index) : base(graph, index) { }
	}
}
