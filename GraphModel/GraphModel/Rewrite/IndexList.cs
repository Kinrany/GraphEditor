using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtensionMethods;

namespace GraphModelLibrary.Rewrite {
	interface INodeIndexList {
		int Count { get; }
		int Last { get; }
		int NewIndex();
		bool Contains(int index);
		void Remove(int index);
		IEnumerator<int> GetEnumerator();
		IEnumerable<Tuple<int, int>> Reindex { get; }
	}

	interface IEdgeIndexList {
		int Count { get; }
		int Last { get; }
		int NewIndex();
		bool Contains(int index);
		void Remove(int index);
		IEnumerator<int> GetEnumerator();
		IEnumerable<Tuple<int, int>> Reindex { get; }
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
				if (_list.Count == 0) {
					return -1;
				}
				else {
					return _list.Last();
				}
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

		public IEnumerable<Tuple<int, int>> Reindex {
			get {
				if (this.Count == this.Last + 1) {
					yield break;
				}

				int n = _list.Count;

				// only values >= n should be moved
				int moveFrom = 0;
				while (_list[moveFrom] < n) {
					moveFrom++;
				}
				
				int moveTo = 0;
				while (moveFrom < _list.Count) {
					while (_list[moveTo] == moveTo) {
						moveTo++;
					}

					int old_index = _list[moveFrom];
					int new_index = moveTo;
					_list.RemoveAt(moveFrom);
					_list.Insert(moveTo, moveTo);
					yield return new Tuple<int, int>(old_index, new_index);

					// editing _list on the fly doesn't break indices here
					// because one element is removed and one added

					moveFrom++;
				}
			}
		}


		private List<int> _list;
	}
}
