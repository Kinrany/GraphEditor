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
			_nodeIndices = new NodeIndexList();
			_edgeIndices = new EdgeIndexList();
			_outgoingEdges = new Dictionary<NodeIndex, HashSet<EdgeIndex>>();
			_incomingEdges = new Dictionary<NodeIndex, HashSet<EdgeIndex>>();
			_edges = new Dictionary<EdgeIndex, Tuple<NodeIndex, NodeIndex>>();
		}

		/// <summary>
		/// Number of nodes in this graph.
		/// </summary>
		public int NodeCount {
			get {
				return _nodeIndices.Count;
			}
		}

		/// <summary>
		/// Number of edges in this graph.
		/// </summary>
		public int EdgeCount {
			get {
				return _edgeIndices.Count;
			}
		}

		public event Action GraphChanged;

		public event Action NodeCreated;
		public event Action NodeDeleted;
		public event Action EdgeCreated;
		public event Action EdgeDeleted;

		/// <summary>
		/// Create a new node.
		/// </summary>
		/// <returns>Index of the created node.</returns>
		public NodeIndex CreateNode() {
			NodeIndex nodeIndex = _nodeIndices.NewIndex();
			_outgoingEdges[nodeIndex] = new HashSet<EdgeIndex>();
			_incomingEdges[nodeIndex] = new HashSet<EdgeIndex>();

			FireNodeCreated();

			return nodeIndex;
		}

		/// <summary>
		/// Create a new edge.
		/// </summary>
		/// <param name="nodeFromIndex">Index of the first node.</param>
		/// <param name="nodeToIndex">Index of the second node.</param>
		/// <returns>Index of the created edge.</returns>
		public EdgeIndex CreateEdge(NodeIndex nodeFromIndex, NodeIndex nodeToIndex) {
			EdgeIndex edgeIndex = _edgeIndices.NewIndex();
			_outgoingEdges[nodeFromIndex].Add(edgeIndex);
			_incomingEdges[nodeToIndex].Add(edgeIndex);
			_edges[edgeIndex] = new Tuple<NodeIndex, NodeIndex>(nodeFromIndex, nodeToIndex);

			FireEdgeCreated();

			return edgeIndex;
		}
		
		/// <summary>
		/// Delete the edge by it's index.
		/// </summary>
		/// <param name="edgeIndex">Edge index.</param>
		public void DeleteEdge(EdgeIndex edgeIndex) {
			var edge = _edges[edgeIndex];
			_outgoingEdges[edge.Item1].Remove(edgeIndex);
			_incomingEdges[edge.Item2].Remove(edgeIndex);
			_edges.Remove(edgeIndex);

			_edgeIndices.Remove(edgeIndex);

			FireEdgeDeleted();
		}

		/// <summary>
		/// Delete the node by it's index.
		/// </summary>
		/// <param name="nodeIndex">Node index.</param>
		public void DeleteNode(NodeIndex nodeIndex) {
			List<EdgeIndex> edgesToDelete = _outgoingEdges[nodeIndex]
				.Concat(_incomingEdges[nodeIndex])
				.Distinct()
				.ToList();

			foreach (EdgeIndex edgeIndex in edgesToDelete) {
				this.DeleteEdge(edgeIndex);
			}

			_nodeIndices.Remove(nodeIndex);

			FireNodeDeleted();
		}

		public NodeIndex GetNodeFrom(EdgeIndex edgeIndex) {
			return _edges[edgeIndex].Item1;
		}

		public NodeIndex GetNodeTo(EdgeIndex edgeIndex) {
			return _edges[edgeIndex].Item2;
		}

		public bool ContainsNode(NodeIndex nodeIndex) {
			return _nodeIndices.Contains(nodeIndex);
		}

		public bool ContainsEdge(EdgeIndex edgeIndex) {
			return _edgeIndices.Contains(edgeIndex);
		}

		public IEnumerable<NodeIndex> NodeEnumerator {
			get {
				foreach (NodeIndex nodeIndex in _nodeIndices) {
					yield return nodeIndex;
				}
			}
		}

		public IEnumerable<EdgeIndex> EdgeEnumerator {
			get {
				foreach (EdgeIndex edgeIndex in _edgeIndices) {
					yield return edgeIndex;
				}
			}
		}

		public IEnumerable<EdgeIndex> OutgoingEnumerator(NodeIndex nodeIndex) {
			foreach (EdgeIndex edgeIndex in _outgoingEdges[nodeIndex]) {
				yield return edgeIndex;
			}
		}

		public IEnumerable<EdgeIndex> IncomingEnumerator(NodeIndex nodeIndex) {
			foreach (EdgeIndex edgeIndex in _incomingEdges[nodeIndex]) {
				yield return edgeIndex;
			}
		}


		protected void FireGraphChanged() {
			GraphChanged?.Invoke();
		}


		private IIndexList<NodeIndex> _nodeIndices;
		private IIndexList<EdgeIndex> _edgeIndices;
		private Dictionary<NodeIndex, HashSet<EdgeIndex>> _outgoingEdges;
		private Dictionary<NodeIndex, HashSet<EdgeIndex>> _incomingEdges;
		private Dictionary<EdgeIndex, Tuple<NodeIndex, NodeIndex>> _edges;

		private void FireNodeCreated() {
			NodeCreated?.Invoke();
			FireGraphChanged();
		}
		private void FireNodeDeleted() {
			NodeDeleted?.Invoke();
			FireGraphChanged();
		}
		private void FireEdgeCreated() {
			EdgeCreated?.Invoke();
			FireGraphChanged();
		}
		private void FireEdgeDeleted() {
			EdgeDeleted?.Invoke();
			FireGraphChanged();
		}
	}
}
