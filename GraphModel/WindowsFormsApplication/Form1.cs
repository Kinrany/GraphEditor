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

		// buttons
		private void loadGraphButton_Click(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			DialogResult result = openFileDialog.ShowDialog();
			if (result == DialogResult.OK) {
				string path = openFileDialog.FileName;
				GraphModel = GraphModel.Load(path);
			}
		}
		private void loadExampleButton_Click(object sender, EventArgs e) {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-4.txt");
			GraphModel = GraphModel.Load(path);
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
					GraphModel.Save(path);
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

		Point ConvertPoint(Point p, Control c1, Control c2) {
			return c2.PointToClient(c1.PointToScreen(p));
		}

		/*[UnmanagedFunctionPointer(CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.I4)]
        private delegate int main_t(
        [MarshalAs(UnmanagedType.I4)] int index,
        string message);*/

		[UnmanagedFunctionPointer(CallingConvention.Winapi)]
		[return: MarshalAs(UnmanagedType.I4)]
		private delegate int mult_t([MarshalAs(UnmanagedType.I4)] int num);

		[UnmanagedFunctionPointer(CallingConvention.Winapi)]
		[return: MarshalAs(UnmanagedType.I4)]
		private delegate int main_t([MarshalAs(UnmanagedType.I4)] int num, string path);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		private delegate string solve_t([MarshalAs(UnmanagedType.LPStr)] string path);

		[UnmanagedFunctionPointer(CallingConvention.Winapi)]
		[return: MarshalAs(UnmanagedType.I4)]
		private delegate string disp_t([MarshalAs(UnmanagedType.LPStr)] string path);

		[UnmanagedFunctionPointer(CallingConvention.Winapi)]
		[return: MarshalAs(UnmanagedType.I4)]
		private delegate void print_t([MarshalAs(UnmanagedType.LPStr)]string path);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		private delegate string adv_print_t([MarshalAs(UnmanagedType.LPStr)]string path);

		//[DllImport(@"C:\Users\Vladimir\Desktop\dlltest\CCode\tryhard\x64\Release\tryhard.dll")]
		//public static extern int MultiplyByTen(int num);

		private void CodeImport_Click(object sender, EventArgs e) {
			//private const string DllFilePath = @"c:\pathto\mydllfile.dll";

			OpenFileDialog openFileDialog = new OpenFileDialog();
			DialogResult result = openFileDialog.ShowDialog();
			if (result == DialogResult.OK) {
				string path = openFileDialog.FileName;
				string path_to_graph = @"C:\Users\Vladimir\Desktop\Diploma\GraphEditor\GraphModel\WindowsFormsApplication\Examples\exampleA1-3.txt";

				/*dynaloader loader = new dynaloader(path);
                main_t main = loader.load_function<main_t>("main");
                main(1, path_to_graph);*/

				dynaloader loader = new dynaloader(path);
				solve_t solve = loader.load_function<solve_t>("solve");
				//MessageBox.Show(solve(path_to_graph));

				GraphModel = GraphModel.Load(solve(path_to_graph));

				/*dynaloader loader = new dynaloader(path);
                adv_print_t solve = loader.load_function<adv_print_t>("adv_print");
                MessageBox.Show(solve(path_to_graph)); */

				//dynaloader loader = new dynaloader(path);
				//adv_print_t printer = loader.load_function<adv_print_t>("adv_print");
				//printer(path_to_graph);

				/*dynaloader loader = new dynaloader(path);
                mult_t main = loader.load_function<mult_t>("MultiplyByTen");
                MessageBox.Show(main(10).ToString());

                dynaloader loader2 = new dynaloader(path2);
                mult_t main2 = loader2.load_function<mult_t>("MultiplyByTwelve");
                mult_t main3 = loader2.load_function<mult_t>("MultiplyByHundred");
                MessageBox.Show(main2(10).ToString());
                MessageBox.Show(main3(65536).ToString());*/


			}
		}
	}

	class dynaloader {
		private IntPtr m_dll = IntPtr.Zero;

		public dynaloader() {
		}

		public dynaloader(string dll_name) {
			load(dll_name);
		}

		~dynaloader() {
			if (loaded())
				unload();
		}

		public bool loaded() {
			return m_dll != IntPtr.Zero;
		}

		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string dllToLoad);

		public bool load(string name) {
			if (loaded())
				unload();
			m_dll = LoadLibrary(name);
			MessageBox.Show(loaded().ToString());
			return loaded();
		}

		[DllImport("kernel32.dll")]
		private static extern bool FreeLibrary(IntPtr hModule);

		public bool unload() {
			if (!loaded())
				return true;
			if (!FreeLibrary(m_dll))
				return false;
			m_dll = IntPtr.Zero;
			return true;
		}

		[DllImport("kernel32.dll")]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

		public T load_function<T>(string name) where T : class {
			IntPtr address = GetProcAddress(m_dll, name);
			MessageBox.Show(address.ToString());
			if (address == IntPtr.Zero)
				return null;
			System.Delegate fn_ptr = Marshal.GetDelegateForFunctionPointer(address, typeof(T));
			return fn_ptr as T;
		}
	}
}
