﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary
{
	public class EditTool {

		public EditTool(Mouse mouse, Keyboard keyboard) {
			this._state = new EmptyState(this);
			this._selectionManager = new Selection(this);

			mouse.LeftClick += (p => MouseLeftClick(p));
			mouse.RightClick += (p => MouseRightClick(p));
			mouse.LeftPressed += (p => MouseLeftPressed(p));
			mouse.LeftDepressed += (p => MouseLeftDepressed(p));
			mouse.Moved += (p => State.MouseMoved(p));
			this._mouse = mouse;

			keyboard.KeyPressed += KeyPressed;
			this._keyboard = keyboard;

			this._graphView = new GraphView();
		}

		public GraphModel Graph {
			get {
				return _graph;
			}
			set {
				_graph = value ?? new GraphModel();
				this.State = new DefaultState(this);
			}
		}
		public GraphView GraphView {
			get {
				return _graphView;
			}
		}
		public EditToolState State {
			get {
				return _state;
			}
			set {
				_state = value;
			}
		}
		public Keyboard Keyboard {
			get {
				return _keyboard;
			}
		}
		public Selection Selection {
			get {
				return _selectionManager;
			}
		}
		public Color PickedColor {
			get {
				return _pickedColor;
			}
			set {
				_pickedColor = value;
			}
		}

		public void Draw(DrawingContext context) {
			this.GraphView.Draw(this.Graph, context);
			State.Draw(context);
			DrawSelected(context);
		}

		readonly Mouse _mouse;
		readonly Keyboard _keyboard;
		readonly Selection _selectionManager;
		GraphModel _graph = new GraphModel();
		GraphView _graphView = new GraphView();
		EditToolState _state;

		Color _pickedColor = Color.Red;

		void MouseLeftClick(Point p) {
			Object o = GraphView?.FindClicked(this.Graph, p);

			NodeModel node = o as NodeModel;
			if (node != null) {
				State.MouseLeftClick(node);
				return;
			}

			State.MouseLeftClick(p);
		}
		void MouseRightClick(Point p) {
			Object o = GraphView?.FindClicked(this.Graph, p);

			NodeModel node = o as NodeModel;
			if (node != null) {
				State.MouseRightClick(node);
				return;
			}

			State.MouseRightClick(p);
		}
		void MouseLeftPressed(Point p) {
			Object o = GraphView?.FindClicked(this.Graph, p);

			NodeModel node = o as NodeModel;
			if (node != null) {
				State.MouseLeftPressed(node);
				return;
			}

			State.MouseLeftPressed(p);
		}
		void MouseLeftDepressed(Point p) {
			Object o = GraphView?.FindClicked(this.Graph, p);

			NodeModel node = o as NodeModel;
			if (node != null) {
				State.MouseLeftDepressed(node);
				return;
			}

			State.MouseLeftDepressed(p);
		}
		void KeyPressed(Keyboard.Key key) {
			State.KeyPressed(key);
		}

		void DrawSelected(DrawingContext context) {
			Pen pen = new Pen(Color.DarkBlue, 1);
			foreach (NodeModel node in Selection) {
				context.DrawCircle(node.Weight.Location, GraphView.NodeRadius + 1, pen);
			}
		}
	}
}
