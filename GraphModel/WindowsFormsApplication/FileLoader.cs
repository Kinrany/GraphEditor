using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphModelLibrary.Rewrite;


namespace WindowsFormsApplication {
	class FileLoader {
		public static void LoadMatrix(string path, DataGridView DataGridMatrix) {
			GraphModel graph = GraphModelParser.Load(path);

			DataGridMatrix.Rows.Clear();
			DataGridMatrix.Refresh();

			int N = graph.NodeCount;
			DataGridMatrix.ColumnCount = N;
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
				
				DataGridMatrix.Rows.Add(row);
			}
		}

		public static void LoadText(string path, RichTextBox TextBox) {
			int counter = 0;
			string line;
			string tmp = null;

			System.IO.StreamReader file = new System.IO.StreamReader(path);

			while ((line = file.ReadLine()) != null) {
				counter++;
				if (line == "Text:") {
					while ((line = file.ReadLine()) != null) {
						tmp += line;
					}
				}
				if (tmp != null) {
					TextBox.Text = tmp;
				}
			}

			file.Close();
		}
	}
}
