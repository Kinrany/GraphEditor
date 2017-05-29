using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {
	public class Selection : IEnumerable<NodeModel> {
		public Selection(EditTool editTool) {
			this._selectedNodes = new HashSet<NodeModel>();
			this._editTool = editTool;
		}

		public void Add(ICollection<NodeModel> collection) {
			foreach (NodeModel node in collection) {
				_selectedNodes.Add(node);
			}
		}
		public void Add(params NodeModel[] nodes) {
			foreach (NodeModel node in nodes) {
				_selectedNodes.Add(node);
			}
		}
		public void Add(Rectangle rect) {
			var list = new List<NodeModel>();
			foreach (NodeModel node in NodeModel.Enumerate(this.Graph)) {
				if (rect.Contains(node.Weight.Location)) {
					list.Add(node);
				}
			}
			Add(list);
		}
		public void Clear() {
			_selectedNodes.Clear();
		}
		public void Delete() {
			foreach (NodeModel node in _selectedNodes) {
				node.Delete();
			}
			_selectedNodes.Clear();
		}
		public void Set(ICollection<NodeModel> collection) {
			Clear();
			Add(collection);
		}
		public void Set(params NodeModel[] nodes) {
			Clear();
			Add(nodes);
		}
		public void Set(Rectangle rect) {
			Clear();
			Add(rect);
		}

		public IEnumerator GetEnumerator() {
			return _selectedNodes.GetEnumerator();
		}
		
		
		private readonly HashSet<NodeModel> _selectedNodes;
		private readonly EditTool _editTool;

		private GraphModel Graph { get { return _editTool.GraphView.Graph; } }

		IEnumerator<NodeModel> IEnumerable<NodeModel>.GetEnumerator() {
			return _selectedNodes.GetEnumerator();
		}
	}
}
