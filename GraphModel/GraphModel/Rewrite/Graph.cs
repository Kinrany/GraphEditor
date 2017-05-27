using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphModelLibrary.Rewrite {

	/// <summary>
	/// Directed multigraph.
	/// </summary>
	public class Graph {

		/// <summary>
		/// Create an empty graph.
		/// </summary>
		public Graph() {
			_nodeIndices = new IndexList();
			_edgeIndices = new IndexList();
			_outgoingEdges = new Dictionary<int, HashSet<int>>();
			_incomingEdges = new Dictionary<int, HashSet<int>>();
			_edges = new Dictionary<int, Tuple<int, int>>();
		}

		/// <summary>
		/// Create a new node.
		/// </summary>
		/// <returns>Index of the created node.</returns>
		public int CreateNode() {
			int nodeIndex = _nodeIndices.NewIndex();
			_outgoingEdges[nodeIndex] = new HashSet<int>();
			_incomingEdges[nodeIndex] = new HashSet<int>();
			return nodeIndex;
		}

		/// <summary>
		/// Create a new edge.
		/// </summary>
		/// <param name="nodeFromIndex">Index of the first node.</param>
		/// <param name="nodeToIndex">Index of the second node.</param>
		/// <returns>Index of the created edge.</returns>
		public int CreateEdge(int nodeFromIndex, int nodeToIndex) {
			int edgeIndex = _edgeIndices.NewIndex();
			_outgoingEdges[nodeFromIndex].Add(edgeIndex);
			_incomingEdges[nodeToIndex].Add(edgeIndex);
			_edges[edgeIndex] = new Tuple<int, int>(nodeFromIndex, nodeToIndex);
			return edgeIndex;
		}
		
		/// <summary>
		/// Delete the edge by it's index.
		/// </summary>
		/// <param name="edgeIndex">Edge index.</param>
		public void DeleteEdge(int edgeIndex) {
			var edge = _edges[edgeIndex];
			_outgoingEdges[edge.Item1].Remove(edgeIndex);
			_incomingEdges[edge.Item2].Remove(edgeIndex);
			_edges.Remove(edgeIndex);

			_edgeIndices.Remove(edgeIndex);
		}

		/// <summary>
		/// Delete the node by it's index.
		/// </summary>
		/// <param name="nodeIndex">Node index.</param>
		public void DeleteNode(int nodeIndex) {
			foreach (int outgoingEdgeIndex in _outgoingEdges[nodeIndex]) {
				var edge = _edges[outgoingEdgeIndex];
				_incomingEdges[edge.Item2].Remove(outgoingEdgeIndex);
				_edges.Remove(outgoingEdgeIndex);
			}
			_outgoingEdges.Remove(nodeIndex);

			foreach (int incomingEdgeIndex in _incomingEdges[nodeIndex]) {
				var edge = _edges[incomingEdgeIndex];
				_outgoingEdges[edge.Item1].Remove(incomingEdgeIndex);
				_edges.Remove(incomingEdgeIndex);
			}
			_incomingEdges.Remove(nodeIndex);

			_nodeIndices.Remove(nodeIndex);
		}

		public EdgeProxy CreateEdgeProxy(int nodeFromIndex, int nodeToIndex) {
			int edgeIndex = this.CreateEdge(nodeFromIndex, nodeToIndex);
			return new EdgeProxy(this, edgeIndex);
		}

		public NodeProxy CreateNodeProxy() {
			int nodeIndex = this.CreateNode();
			return new NodeProxy(this, nodeIndex);
		}

		public void Reindex() {
			throw new NotImplementedException();
		}

		private INodeIndexList _nodeIndices;
		private IEdgeIndexList _edgeIndices;
		private Dictionary<int, HashSet<int>> _outgoingEdges;
		private Dictionary<int, HashSet<int>> _incomingEdges;
		private Dictionary<int, Tuple<int, int>> _edges;
	}
}
