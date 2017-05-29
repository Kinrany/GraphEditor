using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphModelLibrary.Rewrite;
using ExtensionMethods;
using UILogicLibrary;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		public GraphModel GraphModel {
			get {
				return this.GraphView.Graph;
			}
		}
		public GraphView GraphView {
			get {
				return this._editTool.GraphView;
			}
		}

		public event MyDelegate GraphModelChanged;
		public delegate void MyDelegate();

		protected override CreateParams CreateParams {
			get {
				var cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
				return cp;
			}
		}

		private EditTool _editTool = null;
		private Timer _timer;

		private void Form1_Load(object sender, EventArgs e) {
			GraphModelChanged += () => { saveButtonLabel.Text = ""; };

			var mouse = new Mouse();
			graphBox.MouseDown += (s, a) => {
				Point location = a.Location;
				switch (a.Button) {
				case MouseButtons.Left:
					mouse.LeftButtonDown(location);
					break;
				case MouseButtons.Right:
					mouse.RightButtonDown(location);
					break;
				}
			};
			graphBox.MouseUp += (s, a) => {
				Point location = a.Location;
				switch (a.Button) {
				case MouseButtons.Left:
					mouse.LeftButtonUp(location);
					break;
				case MouseButtons.Right:
					mouse.RightButtonUp(location);
					break;
				}
			};
			graphBox.MouseMove += (s, a) => {
				Point location = a.Location;
				mouse.MouseMoved(location);
			};

			this.KeyPreview = true;
			var keyboard = new ConcreteKeyboard(this);

			_editTool = new EditTool(mouse, keyboard);


			graphBox.Paint += graphBox_Draw;

			_timer = new Timer();
			_timer.Interval = 1000 / 30;
			_timer.Tick += (s, h) => graphBox.Invalidate();
			_timer.Start();

			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-3.txt");
			SetGraphModel(GraphModelParser.Load(path));
			FileLoader.LoadMatrix(path, DataGridMatrix);
			FileLoader.LoadText(path, TextBox);
		}
		// buttons
		private void loadGraphButton_Click(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			DialogResult result = openFileDialog.ShowDialog();
			if (result == DialogResult.OK) {
				string path = openFileDialog.FileName;
				this.SetGraphModel(GraphModelParser.Load(path));
			}
		}
		private void loadExampleButton_Click(object sender, EventArgs e) {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-4.txt");
			this.SetGraphModel(GraphModelParser.Load(path));
		}
		private void saveButton_Click(object sender, EventArgs e) {
			if (GraphModel == null) {
				saveButtonLabel.Text = "Сначала нужно открыть граф";
			}
			else {
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				DialogResult result = saveFileDialog.ShowDialog();
				if (result == DialogResult.OK) {
					string path = saveFileDialog.FileName;
					GraphModelParser.Save(this.GraphModel, path);
				}
			}
		}

		private void graphBox_Draw(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			Point mouse = graphBox.PointToClient(Cursor.Position);

			DrawingContext context = new DrawingContext(g, mouse);
			g.FillRegion(Brushes.Beige, g.Clip);
			drawGraph(context);
			_editTool.Draw(context);

			debugLabel.Text = _editTool.State.ToString();
		}

		private void SetGraphModel(GraphModel graph) {
			_editTool.GraphView = new GraphView(graph);
			GraphModelChanged();
		}

		//Legacy mult
		[UnmanagedFunctionPointer(CallingConvention.Winapi)]
		[return: MarshalAs(UnmanagedType.I4)]
		private delegate int mult_t([MarshalAs(UnmanagedType.I4)] int num);

		//Legacy print
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		private delegate string adv_print_t([MarshalAs(UnmanagedType.LPStr)]string path);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.SysInt)]
		private delegate IntPtr solve_t([MarshalAs(UnmanagedType.LPStr)] string path);

		private void toolStripOpenGraph_Click(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			FileLoader File = new FileLoader();
			DialogResult result = openFileDialog.ShowDialog();
			if (result == DialogResult.OK) {
				string path = openFileDialog.FileName;
				PathToFile = path;
				SetGraphModel(GraphModelParser.Load(path));
				FileLoader.LoadMatrix(path, DataGridMatrix);
				FileLoader.LoadText(path, TextBox);
			}
		}

		private void toolStripSaveGraph_Click(object sender, EventArgs e) {
			if (GraphModel == null) {
				saveButtonLabel.Text = "Сначала нужно открыть граф";
			}
			else {
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				DialogResult result = saveFileDialog.ShowDialog();
				if (result == DialogResult.OK) {
					string path = saveFileDialog.FileName;
					GraphModelParser.Save(this.GraphModel, path);
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
					SetGraphModel(GraphModelParser.Load(path_to_graph_2));
					FileLoader.LoadMatrix(path_to_graph_2, DataGridMatrix);
					FileLoader.LoadText(path_to_graph_2, TextBox);
				}
				catch (Exception ex) {
					MessageBox.Show(ex.ToString());
				}
			}
		}

		private string PathToFile;
	}
}
