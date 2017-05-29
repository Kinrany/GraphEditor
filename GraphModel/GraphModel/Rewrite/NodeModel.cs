using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public class NodeModel : WeightedNodeProxy<GraphModel.NodeWeight, GraphModel.EdgeWeight> {
		public NodeModel(GraphModel graph, int index) : base(graph, index) { }

		public new GraphModel Graph {
			get {
				return (GraphModel)base.Graph;
			}
		}

		public static NodeModel Create(GraphModel graph, GraphModel.NodeWeight weight) {
			int nodeIndex = graph.CreateNode(weight);
			return new NodeModel(graph, nodeIndex);
		}

		public EdgeModel AddOutgoingEdge(NodeModel otherNode, GraphModel.EdgeWeight weight) {
			int edgeIndex = this.Graph.CreateEdge(this.Index, otherNode.Index, weight);
			return new EdgeModel(this.Graph, edgeIndex);
		}

		public EdgeModel AddIncomingEdge(NodeModel otherNode, GraphModel.EdgeWeight weight) {
			int edgeIndex = this.Graph.CreateEdge(otherNode.Index, this.Index, weight);
			return new EdgeModel(this.Graph, edgeIndex);
		}

		public new IEnumerable<EdgeModel> OutgoingEnumerator {
			get {
				foreach (int edgeIndex in this.Graph.OutgoingEnumerator(this.Index)) {
					yield return new EdgeModel(this.Graph, edgeIndex);
				}
			}
		}

		public new IEnumerable<EdgeModel> IncomingEnumerator {
			get {
				foreach (int edgeIndex in this.Graph.IncomingEnumerator(this.Index)) {
					yield return new EdgeModel(this.Graph, edgeIndex);
				}
			}
		}

		public static IEnumerable<NodeModel> Enumerate(GraphModel graph) {
			foreach (int nodeIndex in graph.NodeEnumerator) {
				yield return new NodeModel(graph, nodeIndex);
			}
		}
	}
}
