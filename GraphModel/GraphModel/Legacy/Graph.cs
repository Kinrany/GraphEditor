using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Legacy {
	public class Graph : IEnumerable<Node> {
		public Graph() {
			_list = new List<Node>();
		}

		public int Count {
			get {
				return _list.Count;
			}
		}

		public void Add(Node node) {
			_list.Add(node);
		}
		[Obsolete]
		public Node AddNode() {
			Node node = new Node();
			_list.Add(node);
			return node;
		}

		public void Clear() {
			_list.Clear();
		}

		public bool Contains(Node node) {
			return _list.Contains(node);
		}

		public IEnumerator<Node> GetEnumerator() {
			return _list.GetEnumerator();
		}

		public bool Remove(Node node) {
			return _list.Remove(node);
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return _list.GetEnumerator();
		}

		List<Node> _list;
	}
}
