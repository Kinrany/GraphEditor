﻿using System;
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
		private Timer _timer;

		private string PathToFile;


		// loading
		private void Form1_Load(object sender, EventArgs e) {
			Mouse mouse = LoadMouse();
			ConcreteKeyboard keyboard = LoadKeyboard();
			_editTool = new EditTool(mouse, keyboard);

			this._timer = LoadTimer();

			this.SetGraphModel(this.GraphModel);
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
			this.GraphModel.ChangedEvent -= OnGraphModelChanged;

			_editTool.Graph = graph;

			graph.ChangedEvent += OnGraphModelChanged;
			OnGraphModelChanged();

			saveButtonLabel.Text = "";
		}
		private void OnGraphModelChanged() {
			MatrixUpdater.UpdateMatrix(this.GraphModel, DataGridMatrix);
			TextBox.Text = this.GraphModel.Text;
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
			colorDialog.CustomColors = Helper.IntToColor
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
			g.FillRegion(Brushes.Beige, g.Clip);
			_editTool.Draw(context);

			debugLabel.Text = _editTool.State.ToString();

			var bounds = g.VisibleClipBounds;
			var brush = new SolidBrush(_editTool.PickedColor);
			g.FillRectangle(brush, bounds.Right - 15, 0, 15, 15);
		}
		private void TextBox_TextChanged(object sender, EventArgs e) {
			RichTextBox textBox = (RichTextBox)sender;
			GraphModel.Text = textBox.Text;
		}
		private void DataGridMatrix_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
			// HACK
			// e.RowIndex and e.ColumnIndex do not match to real node indices

			NodeIndex nodeFromIndex = new NodeIndex(e.RowIndex);
			NodeIndex nodeToIndex = new NodeIndex(e.ColumnIndex);
			EdgeIndex? edgeIndex = GraphModel.GetEdgeBetween(nodeFromIndex, nodeToIndex);
			string value = (sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value.ToString();

			EdgeModel edge;
			if (edgeIndex != null) {
				edge = new EdgeModel(GraphModel, (EdgeIndex)edgeIndex);
			}
			else {
				edge = EdgeModel.Create(GraphModel, nodeFromIndex, nodeToIndex);
			}

			edge.Weight.Value = value;
		}
	}
}
