using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using GraphModelLibrary.Rewrite;
using ExtensionMethods;
using UILogicLibrary;

namespace WindowsFormsApplication {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		public GraphModel GraphModel {
			get {
				return _editTool.Graph;
			}
		}

		protected override CreateParams CreateParams {
			get {
				var cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
				return cp;
			}
		}

		private EditTool _editTool = null;
		private DataGridContainer _dataGridContainer;

		private string PathToFile;


		// loading
		private void Form1_Load(object sender, EventArgs e) {
			_dataGridContainer = new DataGridContainer(DataGridMatrix);

			Mouse mouse = LoadMouse();
			ConcreteKeyboard keyboard = LoadKeyboard();
			_editTool = new EditTool(mouse, keyboard);

			SetGraphModel(this.GraphModel);
		}

		private Mouse LoadMouse() {
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
			return mouse;
		}
		private ConcreteKeyboard LoadKeyboard() {
			this.KeyPreview = true;
			var keyboard = new ConcreteKeyboard(this);
			return keyboard;
		}

		private Timer LoadTimer() {
			// automatically invalidates graphBox every X milliseconds
			Timer timer = new Timer();
			timer.Interval = 1000 / 30;
			timer.Tick += (s, h) => graphBox.Invalidate();
			timer.Start();
			return timer;
		}

		private void SetGraphModel(GraphModel graph) {
			GraphModel.GraphChanged -= OnGraphModelChanged;

			_editTool.Graph = graph;

			graph.GraphChanged += OnGraphModelChanged;
			OnGraphModelChanged();

			saveButtonLabel.Text = "";
		}
		private void OnGraphModelChanged() {
			_dataGridContainer.Update(this.GraphModel);
			TextBox.Text = this.GraphModel.Text;
			graphBox.Invalidate();
		}

		
		// user interface
		private void coloringModeButton_CheckedChanged(object sender, EventArgs e) {
			RadioButton button = (RadioButton)sender;
			if (button.Checked) {
				_editTool.State = new ColoringState(_editTool);
			}
		}
		private void defaultModeButton_CheckedChanged(object sender, EventArgs e) {
			RadioButton button = (RadioButton)sender;
			if (button.Checked) {
				_editTool.State = new DefaultState(_editTool);
			}
		}

		private void colorPickerButton_Click(object sender, EventArgs e) {
			colorDialog.CustomColors = _editTool.GraphView.Stylesheet.Colors
				.Take(16)
				.Select((x) => Color.FromArgb(0,x.B,x.G,x.R).ToArgb())
				.ToArray();
			colorDialog.ShowDialog();
			this._editTool.PickedColor = colorDialog.Color;
		}


		// stuff
		private void graphBox_Draw(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			Point mouse = graphBox.PointToClient(Cursor.Position);

			DrawingContext context = new DrawingContext(g, mouse);
			g.FillRegion(Brushes.WhiteSmoke, g.Clip);
			_editTool.Draw(context);

			debugLabel.Text = _editTool.State.ToString();

			var bounds = g.VisibleClipBounds;

			var pickedColorBrush = new SolidBrush(_editTool.PickedColor);
			var pickedColorBoxSize = new SizeF(15, 15);
			var pickedColorBoxLocation = new PointF(bounds.Right - pickedColorBoxSize.Width, 0);
			var pickedColorBounds = new RectangleF(pickedColorBoxLocation, pickedColorBoxSize);
			g.FillRectangle(pickedColorBrush, pickedColorBounds);

			var borderPen = new Pen(Color.Black, 1.5f);
			var borderLocation = bounds.Location;
			var borderSize = bounds.Size - new SizeF(1, 1);
			var borderRect = new RectangleF(borderLocation, borderSize);
			g.DrawRectangle(borderPen, borderRect);
		}
		private void TextBox_TextChanged(object sender, EventArgs e) {
			RichTextBox textBox = (RichTextBox)sender;
			GraphModel.Text = textBox.Text;
		}

		private void graphBox_MouseMove(object sender, MouseEventArgs e) {
			graphBox.Invalidate();
		}
	}
}
