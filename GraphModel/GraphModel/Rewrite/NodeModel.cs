using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class NodeModel : WeightedNodeProxy<GraphModel.NodeWeight, GraphModel.EdgeWeight> {
		public NodeModel(GraphModel graph, int index) : base(graph, index) { }
	}
}
