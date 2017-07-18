using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	using NodeWeight = GraphModel.NodeWeight;
	using EdgeWeight = GraphModel.EdgeWeight;

	public class NodeModel : WeightedNodeProxy<NodeWeight, EdgeWeight> {
		public NodeModel(GraphModel graph, NodeIndex index) : base(graph, index) { }

		public new GraphModel Graph {
			get {
				return (GraphModel)base.Graph;
			}
		}

		public static NodeModel Create(GraphModel graph, NodeWeight weight) {
			NodeIndex nodeIndex = graph.CreateNode(weight);
			return new NodeModel(graph, nodeIndex);
		}

		public EdgeModel AddOutgoingEdge(NodeModel otherNode, EdgeWeight weight = null) {
			if (weight == null) {
				weight = new EdgeWeight();
			}

			EdgeIndex edgeIndex = this.Graph.CreateEdge(this.Index, otherNode.Index, weight);
			return new EdgeModel(this.Graph, edgeIndex);
		}

		public EdgeModel AddIncomingEdge(NodeModel otherNode, EdgeWeight weight = null) {
			if (weight == null) {
				weight = new EdgeWeight();
			}

			EdgeIndex edgeIndex = this.Graph.CreateEdge(otherNode.Index, this.Index, weight);
			return new EdgeModel(this.Graph, edgeIndex);
		}

		public new IEnumerable<EdgeModel> OutgoingEnumerator {
			get {
				foreach (EdgeIndex edgeIndex in this.Graph.OutgoingEnumerator(this.Index)) {
					yield return new EdgeModel(this.Graph, edgeIndex);
				}
			}
		}

		public new IEnumerable<EdgeModel> IncomingEnumerator {
			get {
				foreach (EdgeIndex edgeIndex in this.Graph.IncomingEnumerator(this.Index)) {
					yield return new EdgeModel(this.Graph, edgeIndex);
				}
			}
		}

		public static IEnumerable<NodeModel> Enumerate(GraphModel graph) {
			foreach (NodeIndex nodeIndex in graph.NodeEnumerator) {
				yield return new NodeModel(graph, nodeIndex);
			}
		}
	}
}
