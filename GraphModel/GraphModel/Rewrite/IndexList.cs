using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	interface INodeIndexList {
		int Count { get; }
		int NewIndex();
		bool Contains(int index);
		void Remove(int index);
	}

	interface IEdgeIndexList {
		int Count { get; }
		int NewIndex();
		bool Contains(int index);
		void Remove(int index);
	}

	class IndexList : INodeIndexList, IEdgeIndexList {

		public int Count {
			get {
				return _list.Count;
			}
		}

		public int NewIndex() {
			int newIndex = _list.Last() + 1;
			_list.Add(newIndex);
			return newIndex;
		}

		public bool Contains(int index) {
			return _list.Contains(index);
		}

		public void Remove(int index) {
			_list.Remove(index);
		}


		private List<int> _list;
	}
}
