﻿using System;
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
				column.Name = node.Name;
				_dataGrid.Columns.Add(column);
			}

			foreach (NodeModel nodeFrom in NodeModel.Enumerate(graph)) {
				var row = new DataGridViewRow();
				row.HeaderCell.Value = nodeFrom.Name;

				foreach (NodeModel nodeTo in NodeModel.Enumerate(graph)) {
					EdgeModel edge = EdgeModel.Between(nodeFrom, nodeTo);

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
			string rowName = _dataGrid.Rows[e.RowIndex].HeaderCell.Value.ToString();
			NodeModel nodeFrom = NodeModel.Enumerate(_graph).First(node => node.Name == rowName);

			string columnName = _dataGrid.Columns[e.ColumnIndex].HeaderCell.Value.ToString();
			NodeModel nodeTo = NodeModel.Enumerate(_graph).First(node => node.Name == columnName);

			EdgeModel edge = EdgeModel.Between(nodeFrom, nodeTo);
			string value = _dataGrid[e.ColumnIndex, e.RowIndex].Value.ToString();

			if (edge == null) {
				edge = EdgeModel.Create(nodeFrom, nodeTo);
			}

			var weight = edge.Weight;
			weight.Value = value;
			edge.Weight = weight;
		}
	}
}
