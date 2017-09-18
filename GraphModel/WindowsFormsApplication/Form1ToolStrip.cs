using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GraphModelLibrary.Rewrite;
using UILogicLibrary;

namespace WindowsFormsApplication {
	public partial class Form1 {

		private string _graphFileFilter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

		// tool strip buttons
		private void toolStripOpenA1Graph_Click(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = _graphFileFilter;

			DialogResult result = openFileDialog.ShowDialog();
			if (result == DialogResult.OK) {
				string path = openFileDialog.FileName;

				PathToFile = path;
				string text = File.ReadAllText(path);
				GraphModel graph = GraphModelParser.ParseA1(text);

				SetGraphModel(graph);

				NodeRearrangementAlgorithms.Circle(graph);
			}
		}
		private void toolStripOpenA2Graph_Click(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = _graphFileFilter;

			DialogResult result = openFileDialog.ShowDialog();
			if (result == DialogResult.OK) {
				string path = openFileDialog.FileName;

				PathToFile = path;
				string text = File.ReadAllText(path);
				GraphModel graph = GraphModelParser.ParseA2(text);

				SetGraphModel(graph);

				NodeRearrangementAlgorithms.Circle(graph);
			}
		}
		private void toolStripSaveA1Graph_Click(object sender, EventArgs e) {
			if (GraphModel == null) {
				saveButtonLabel.Text = "Сначала нужно открыть граф";
			}
			else {
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = _graphFileFilter;

				DialogResult result = saveFileDialog.ShowDialog();
				if (result == DialogResult.OK) {
					string path = saveFileDialog.FileName;
					string text = GraphModelParser.SerializeA1(GraphModel);
					File.WriteAllText(path, text);
				}
			}
		}
		private void toolStripSaveA2Graph_Click(object sender, EventArgs e) {
			if (GraphModel == null) {
				saveButtonLabel.Text = "Сначала нужно открыть граф";
			}
			else {
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = _graphFileFilter;

				DialogResult result = saveFileDialog.ShowDialog();
				if (result == DialogResult.OK) {
					string path = saveFileDialog.FileName;
					string text = GraphModelParser.SerializeA2(GraphModel);
					File.WriteAllText(path, text);
				}
			}
		}
		private void toolStripImportCode_Click(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			DialogResult result = openFileDialog.ShowDialog();
			if (result == DialogResult.OK) {
				string path = openFileDialog.FileName;
				string path_to_graph = PathToFile;

				//MessageBox.Show(IntPtr.Size.ToString()); Use later if needed - shows bits of system. If 8 => x64

				try {
					Dynaloader loader = new Dynaloader(path);
					solve_t solve = loader.load_function<solve_t>("solve");
					string path_to_graph_2 = Marshal.PtrToStringAnsi(solve(path_to_graph));
					MessageBox.Show(path_to_graph_2);
					SetGraphModel(GraphModelParser.ParseA1(File.ReadAllText(path_to_graph_2)));
				}
				catch (Exception ex) {
					MessageBox.Show(ex.ToString());
				}
			}
		}
		private void toolStripSaveImage_Click(object sender, EventArgs e) {
			Rectangle rect = graphBox.ClientRectangle;
			Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
			graphBox.DrawToBitmap(bitmap, new Rectangle(0, 0, rect.Width, rect.Height));

			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = ".png";
			dialog.Filter = "PNG files |*.png";
			dialog.ShowDialog();
			var path = dialog.FileName;
			bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
			dialog.Dispose();
		}
		private void toolStripRearrangeCircle_Click(object sender, EventArgs e) {
			NodeRearrangementAlgorithms.Circle(this.GraphModel);
		}

		// code importing delegate
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.SysInt)]
		private delegate IntPtr solve_t([MarshalAs(UnmanagedType.LPStr)] string path);
	}
}