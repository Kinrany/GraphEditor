using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphModelLibrary.Rewrite;

namespace WindowsFormsApplication {
	class DataGridContainer : IDisposable {
		public DataGridContainer(DataGridView dataGrid) {
			Debug.Assert(dataGrid != null);

			_dataGrid = dataGrid;
			_graph = null;

			_dataGrid.CellEndEdit += OnCellEndEdit;
		}

		public void Dispose() {
			_dataGrid.CellEndEdit -= OnCellEndEdit;
		}

		public void Update(GraphModel graph) {
			_graph = graph;

			_dataGrid.Columns.Clear();

			_dataGrid.TopLeftHeaderCell.Value = @"ИЗ \ В";
			_dataGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader);

			foreach (NodeModel node in NodeModel.Enumerate(graph)) {
				var column = new DataGridViewTextBoxColumn();
				column.Name = node.Index.ToString();
				_dataGrid.Columns.Add(column);
			}

			foreach (NodeModel nodeFrom in NodeModel.Enumerate(graph)) {
				var row = new DataGridViewRow();
				row.HeaderCell.Value = nodeFrom.Index.ToString();

				foreach (NodeModel nodeTo in NodeModel.Enumerate(graph)) {
					EdgeModel edge = EdgeModel.Between(graph, nodeFrom, nodeTo);

					row.Cells.Add(new DataGridViewTextBoxCell() {
						Value = (edge == null) ? "" : edge.Weight.Value
					});
				}

				_dataGrid.Rows.Add(row);
			}

			_dataGrid.Refresh();
		}


		private DataGridView _dataGrid;
		private GraphModel _graph;

		private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e) {
			// HACK
			// e.RowIndex and e.ColumnIndex do not match to real node indices

			NodeIndex nodeFromIndex = new NodeIndex(e.RowIndex);
			NodeIndex nodeToIndex = new NodeIndex(e.ColumnIndex);
			EdgeIndex? edgeIndex = _graph.GetEdgeBetween(nodeFromIndex, nodeToIndex);
			string value = (sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value.ToString();

			EdgeModel edge;
			if (edgeIndex != null) {
				edge = new EdgeModel(_graph, (EdgeIndex)edgeIndex);
			}
			else {
				edge = EdgeModel.Create(_graph, nodeFromIndex, nodeToIndex);
			}

			var weight = edge.Weight;
			weight.Value = value;
			edge.Weight = weight;
		}
	}
}
