using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Legacy {
	public class Node {
		public Node() {
			this._incomingEdges = new HashSet<Edge>();
			this._outgoingEdges = new HashSet<Edge>();
		}

		public void AddIncomingEdge(Edge edge) {
			if (edge.To != this) {
				throw new InvalidOperationException();
			}

			_incomingEdges.Add(edge);
		}
		public void AddOutgoingEdge(Edge edge) {
			if (edge.From != this) {
				throw new InvalidOperationException();
			}

			_outgoingEdges.Add(edge);
		}
		public void RemoveIncomingEdge(Edge edge) {
			if (edge.To != this) {
				throw new InvalidOperationException();
			}

			bool success = _incomingEdges.Remove(edge);
			if (!success) {
				throw new InvalidOperationException();
			}
		}
		public void RemoveOutgoingEdge(Edge edge) {
			if (edge.From != this) {
				throw new InvalidOperationException();
			}

			bool success = _outgoingEdges.Remove(edge);
			if (!success) {
				throw new InvalidOperationException();
			}
		}

		public IEnumerable<Edge> GetIncomingEdges() {
			return _incomingEdges as IEnumerable<Edge>;
		}
		public IEnumerable<Node> GetIncomingNodes() {
			return _incomingEdges.Select((edge) => (edge.From));
		}
		public IEnumerable<Edge> GetOutgoingEdges() {
			return _outgoingEdges as IEnumerable<Edge>;
		}
		public IEnumerable<Node> GetOutgoingNodes() {
			return _outgoingEdges.Select((edge) => (edge.To));
		}

		public void Delete() {
			while (_incomingEdges.Count > 0) {
				_incomingEdges.First().Delete();
			}
			while (_outgoingEdges.Count > 0) {
				_outgoingEdges.First().Delete();
			}
		}

		readonly HashSet<Edge> _incomingEdges;
		readonly HashSet<Edge> _outgoingEdges;
	}
}
