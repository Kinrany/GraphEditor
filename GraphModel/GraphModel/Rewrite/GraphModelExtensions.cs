using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public static class GraphModelExtensions {

		public static EdgeIndex[] GetEdgesBetween(this GraphModel graph, NodeIndex nodeFrom, NodeIndex nodeTo) {
			return graph.OutgoingEnumerator(nodeFrom)
				.Where(edgeIndex => graph.GetNodeTo(edgeIndex) == nodeTo)
				.ToArray();
		}

		public static EdgeIndex? GetEdgeBetween(this GraphModel graph, NodeIndex nodeFrom, NodeIndex nodeTo) {
			EdgeIndex[] indices = graph.GetEdgesBetween(nodeFrom, nodeTo);
			if (indices.Length > 0) {
				return indices.First();
			}
			else {
				return null;
			}
		}
	}
}
