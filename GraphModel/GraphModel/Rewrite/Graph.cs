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

		/// <summary>
		/// Index of the last node in this graph.
		/// </summary>
		public int LastNode {
			get {
				return _nodeIndices.Last;
			}
		}

		/// <summary>
		/// Index of the last edge in this graph.
		/// </summary>
		public int LastEdge {
			get {
				return _edgeIndices.Last;
			}
		}

		/// <summary>
		/// Create a new node.
		/// </summary>
		/// <returns>Index of the created node.</returns>
		public int CreateNode() {
			int nodeIndex = _nodeIndices.NewIndex();
			_outgoingEdges[nodeIndex] = new HashSet<int>();
			_incomingEdges[nodeIndex] = new HashSet<int>();

			this.ChangedEvent();

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

			this.ChangedEvent();

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

			this.ChangedEvent();
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

			this.ChangedEvent();
		}

		public int GetNodeFrom(int edgeIndex) {
			return _edges[edgeIndex].Item1;
		}

		public int GetNodeTo(int edgeIndex) {
			return _edges[edgeIndex].Item2;
		}

		public int[] GetEdgesBetween(int nodeFromIndex, int nodeToIndex) {
			return _outgoingEdges[nodeFromIndex]
				.Where(edgeIndex => _edges[edgeIndex].Item2 == nodeToIndex)
				.ToArray();
		}

		public bool ContainsNode(int nodeIndex) {
			return _nodeIndices.Contains(nodeIndex);
		}

		public bool ContainsEdge(int edgeIndex) {
			return _edgeIndices.Contains(edgeIndex);
		}

		public IEnumerable<int> NodeEnumerator {
			get {
				foreach (int nodeIndex in _nodeIndices) {
					yield return nodeIndex;
				}
			}
		}

		public IEnumerable<int> EdgeEnumerator {
			get {
				foreach (int edgeIndex in _edgeIndices) {
					yield return edgeIndex;
				}
			}
		}

		public IEnumerable<int> OutgoingEnumerator(int nodeIndex) {
			foreach (int edgeIndex in _outgoingEdges[nodeIndex]) {
				yield return edgeIndex;
			}
		}

		public IEnumerable<int> IncomingEnumerator(int nodeIndex) {
			foreach (int edgeIndex in _incomingEdges[nodeIndex]) {
				yield return edgeIndex;
			}
		}

		public void Reindex() {
			foreach (var reindexed in _nodeIndices.Reindex) {
				NodeReindexEvent(reindexed.Item1, reindexed.Item2);
			}

			this.ChangedEvent();
		}

		public event Action<int, int> NodeReindexEvent = (int _, int __) => { };

		public event Action ChangedEvent = () => { };

		private INodeIndexList _nodeIndices;
		private IEdgeIndexList _edgeIndices;
		private Dictionary<int, HashSet<int>> _outgoingEdges;
		private Dictionary<int, HashSet<int>> _incomingEdges;
		private Dictionary<int, Tuple<int, int>> _edges;
	}
}
