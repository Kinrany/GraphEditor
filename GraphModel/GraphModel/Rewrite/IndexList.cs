using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	interface INodeIndexList {
		int Count { get; }
		int Last { get; }
		int NewIndex();
		bool Contains(int index);
		void Remove(int index);
		IEnumerator<int> GetEnumerator();
	}

	interface IEdgeIndexList {
		int Count { get; }
		int Last { get; }
		int NewIndex();
		bool Contains(int index);
		void Remove(int index);
		IEnumerator<int> GetEnumerator();
	}

	class IndexList : INodeIndexList, IEdgeIndexList {
		public IndexList() {
			_list = new List<int>();
		}

		public int Count {
			get {
				return _list.Count;
			}
		}

		public int Last {
			get {
				return _list.Last();
			}
		}

		public int NewIndex() {
			int newIndex = this.Last + 1;
			_list.Add(newIndex);
			return newIndex;
		}

		public bool Contains(int index) {
			return _list.Contains(index);
		}

		public void Remove(int index) {
			_list.Remove(index);
		}

		public IEnumerator<int> GetEnumerator() {
			return _list.GetEnumerator();
		}

		private List<int> _list;
	}
}
