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
using GraphModelLibrary;
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
				return _graphModel;
			}
			private set {
				_graphModel = value;
				GraphView = new GraphView(_graphModel.Graph);
				GraphModelChanged();
			}
		}
		public GraphView GraphView {
			get {
				return _graphView;
			}
			private set {
				_graphView = value;
				_editTool.GraphView = _graphView;
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

		private GraphModel _graphModel = null;
		private GraphView _graphView = null;
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

                        foreach(var x in GraphModel.Graph)
                        {
                            //x.GetIncomingEdges();
                            /*foreach(var y in GraphModel.Graph)
                            {
                                if(x == y)
                                {
                                    continue;
                                }
                                else
                                {
                                    x.
                                }
                            }*/
                        }
                        /*MessageBox.Show(GraphModel.Graph._list.Count.ToString());
                        DataGridMatrix.ColumnCount = GraphModel.Graph._list.Count;
                        var row = new DataGridViewRow();
                        for (int columnIndex = 0; columnIndex < DataGridMatrix.ColumnCount + 1; ++columnIndex)
                        {
                            row.Cells.Add(new DataGridViewTextBoxCell()
                            {
                                Value = 0
                            });
                        }
                        DataGridMatrix.ColumnCount = DataGridMatrix.ColumnCount + 1;
                        DataGridMatrix.Rows.Add(row);
                        for (int i = 0; i < DataGridMatrix.ColumnCount; i++)
                        {
                            DataGridMatrix.Rows[i].Cells[DataGridMatrix.ColumnCount - 1].Value = 0;
                        }*/

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

		Point ConvertPoint(Point p, Control c1, Control c2) {
			return c2.PointToClient(c1.PointToScreen(p));
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
        [return: MarshalAs(UnmanagedType.LPStr)]
        private delegate string solve_t([MarshalAs(UnmanagedType.LPStr)] string path);

        private void toolStripOpenGraph_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            FileLoader File = new FileLoader();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                GraphModel = GraphModel.Load(path);
                FileLoader.LoadMatrix(path, DataGridMatrix);
                FileLoader.LoadText(path, TextBox);
            }
        }

        private void toolStripSaveGraph_Click(object sender, EventArgs e)
        {
            if (GraphModel == null)
            {
                saveButtonLabel.Text = "Сначала нужно открыть граф";
            }
            else {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    GraphModel.Save(path);
                }
            }
        }

        private void toolStripImportCode_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                string path_to_graph = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-3.txt");

                //MessageBox.Show(IntPtr.Size.ToString()); Use later if needed - shows bits of system. If 8 => x64

                Dynaloader loader = new Dynaloader(path);
                solve_t solve = loader.load_function<solve_t>("solve");
                string path_to_graph_2 = solve(path_to_graph);
                GraphModel = GraphModel.Load(path_to_graph_2);

                FileLoader.LoadMatrix(path_to_graph_2, DataGridMatrix);
                FileLoader.LoadText(path_to_graph_2, TextBox);
            }
        }
    }
}
