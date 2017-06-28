using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphModelLibrary.Rewrite;


namespace WindowsFormsApplication {
	class MatrixUpdater {
		public static void UpdateMatrix(GraphModel graph, DataGridView dataGridMatrix) {
			dataGridMatrix.Columns.Clear();

			dataGridMatrix.TopLeftHeaderCell.Value = @"FROM \ TO";
			dataGridMatrix.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader);

			foreach (NodeModel node in NodeModel.Enumerate(graph)) {
				var column = new DataGridViewTextBoxColumn();
				column.Name = node.Index.ToString();
				dataGridMatrix.Columns.Add(column);
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

				dataGridMatrix.Rows.Add(row);
			}

			dataGridMatrix.Refresh();
		}
	}
}
