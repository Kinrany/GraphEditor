using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphModelLibrary.Rewrite;


namespace WindowsFormsApplication {
	class MatrixUpdater {
		public static void UpdateMatrix(GraphModel graph, DataGridView dataGridMatrix) {
			dataGridMatrix.Rows.Clear();
			dataGridMatrix.Refresh();

			int N = graph.NodeCount;
			dataGridMatrix.ColumnCount = N;
			var rowCount = N;
			var rowLength = N;

			for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex) {
				var row = new DataGridViewRow();

				for (int columnIndex = 0; columnIndex < rowLength; ++columnIndex) {
					int edgeIndex = (int)graph.GetEdgeBetween(rowIndex, columnIndex);
					row.Cells.Add(new DataGridViewTextBoxCell() {
						Value = graph.GetEdgeWeight(edgeIndex).Value
					});
				}

				dataGridMatrix.Rows.Add(row);
			}
		}
	}
}
