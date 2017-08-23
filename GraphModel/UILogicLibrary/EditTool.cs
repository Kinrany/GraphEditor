using System;
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
				this.Selection.Clear();
				this.State = new DefaultState(this);
			}
		}
		public GraphView GraphView {
			get {
				return _graphView;
			}
		}
		public Keyboard Keyboard {
			get {
				return _keyboard;
			}
		}
		public Color PickedColor {
			get {
				return _graphView.Stylesheet.IdToColor(_pickedColorId);
			}
			set {
				_pickedColorId = _graphView.Stylesheet.ColorToId(value);
			}
		}
		public ColorId PickedColorId {
			get {
				return _pickedColorId;
			}
			set {
				_pickedColorId = value;
			}
		}
		public Selection Selection {
			get {
				return _selectionManager;
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

		public void Draw(DrawingContext context) {
			this.GraphView.Draw(this.Graph, context);
			State.Draw(context);
			DrawSelected(context);
		}

		GraphModel _graph = new GraphModel();
		GraphView _graphView = new GraphView();
		readonly Keyboard _keyboard;
		readonly Mouse _mouse;
		ColorId _pickedColorId = new ColorId(2);
		readonly Selection _selectionManager;
		EditToolState _state;

		void KeyPressed(Keyboard.Key key) {
			State.KeyPressed(key);
		}
		void MouseLeftClick(Point p) {
			Object o = GraphView?.FindClicked(this.Graph, p);

			NodeModel node = o as NodeModel;
			if (node != null) {
				State.MouseLeftClick(node);
				return;
			}

			State.MouseLeftClick(p);
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
		void MouseLeftPressed(Point p) {
			Object o = GraphView?.FindClicked(this.Graph, p);

			NodeModel node = o as NodeModel;
			if (node != null) {
				State.MouseLeftPressed(node);
				return;
			}

			State.MouseLeftPressed(p);
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

		void DrawSelected(DrawingContext context) {
			Pen pen = new Pen(Color.DarkBlue, 1);
			foreach (NodeModel node in Selection) {
				context.DrawCircle(node.Weight.Location, GraphView.NodeRadius + 1, pen);
			}
		}
	}
}
