using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtensionMethods;

namespace GraphModelLibrary.Rewrite {
	interface INodeIndexList {
		int Count { get; }
		NodeIndex NewIndex();
		bool Contains(NodeIndex index);
		void Remove(NodeIndex index);
		IEnumerator<NodeIndex> GetEnumerator();
	}

	public struct NodeIndex {
		public NodeIndex(int value) {
			this.Value = value;
		}

		public static readonly NodeIndex NaN = new NodeIndex(-1);

		public readonly int Value;

		public static implicit operator int(NodeIndex nodeIndex) {
			return nodeIndex.Value;
		}

		public override string ToString() {
			return this.Value.ToString();
		}
	}

	class NodeIndexList : INodeIndexList {
		public NodeIndexList() {
			_list = new List<NodeIndex>();
		}

		public int Count {
			get {
				return _list.Count;
			}
		}

		public NodeIndex NewIndex() {
			NodeIndex newIndex = this.GetNextIndex();
			_list.Add(newIndex);
			return newIndex;
		}

		public bool Contains(NodeIndex index) {
			return _list.Contains(index);
		}

		public void Remove(NodeIndex index) {
			_list.Remove(index);
		}

		public IEnumerator<NodeIndex> GetEnumerator() {
			return _list.GetEnumerator();
		}


		private static readonly NodeIndex START_INDEX = new NodeIndex(0);

		private List<NodeIndex> _list;

		private NodeIndex GetNextIndex() {
			if (_list.Count == 0) {
				return START_INDEX;
			}
			else {
				return new NodeIndex(_list.Last() + 1);
			}
		}
	}



	interface IEdgeIndexList {
		int Count { get; }
		EdgeIndex NewIndex();
		bool Contains(EdgeIndex index);
		void Remove(EdgeIndex index);
		IEnumerator<EdgeIndex> GetEnumerator();
	}

	public struct EdgeIndex {
		public EdgeIndex(int value) {
			this.Value = value;
		}

		public static readonly EdgeIndex NaN = new EdgeIndex(-1);

		public readonly int Value;

		public static implicit operator int(EdgeIndex edgeIndex) {
			return edgeIndex.Value;
		}

		public override string ToString() {
			return this.Value.ToString();
		}
	}

	class EdgeIndexList : IEdgeIndexList {
		public EdgeIndexList() {
			_list = new List<EdgeIndex>();
		}

		public int Count {
			get {
				return _list.Count;
			}
		}

		public EdgeIndex NewIndex() {
			EdgeIndex newIndex = this.GetNextIndex();
			_list.Add(newIndex);
			return newIndex;
		}

		public bool Contains(EdgeIndex index) {
			return _list.Contains(index);
		}

		public void Remove(EdgeIndex index) {
			_list.Remove(index);
		}

		public IEnumerator<EdgeIndex> GetEnumerator() {
			return _list.GetEnumerator();
		}


		private static readonly EdgeIndex START_INDEX = new EdgeIndex(0);

		private List<EdgeIndex> _list;

		private EdgeIndex GetNextIndex() {
			if (_list.Count == 0) {
				return START_INDEX;
			}
			else {
				return new EdgeIndex(_list.Last() + 1);
			}
		}
	}
}
