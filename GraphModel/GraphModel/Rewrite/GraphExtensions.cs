using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public static class GraphExtensions {
		public static EdgeIndex[] GetEdgesBetween(this Graph graph, NodeIndex nodeFrom, NodeIndex nodeTo) {
			return graph.OutgoingEnumerator(nodeFrom)
				.Where(edgeIndex => graph.GetNodeTo(edgeIndex) == nodeTo)
				.ToArray();
		}
	}
}
