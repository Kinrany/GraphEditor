﻿using System;
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
	}
}
