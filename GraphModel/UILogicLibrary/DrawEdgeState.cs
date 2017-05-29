using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary;

namespace UILogicLibrary {
	class DrawEdgeState : EditToolState {
		public DrawEdgeState(EditTool editTool, NodeModel start) : base(editTool) {
			this._start = start;
		}

		public override void Draw(DrawingContext context) {
			context.DrawArrow(_start.Location, context.MousePosition);
		}

		public override void MouseLeftClick(Point location) {
			CurrentState = new DefaultState(EditTool);
		}

		public override void MouseLeftClick(NodeModel node) {
			EdgeModel edge = new EdgeModel(_start, node, _defaultNodeColor);
			_start.AddOutgoingEdge(edge);
			CurrentState = new DefaultState(EditTool);
		}

		public override void MouseRightClick(Point location) {
			CurrentState = new DefaultState(EditTool);
		}

		public override void MouseRightClick(NodeModel node) {
			CurrentState = new DefaultState(EditTool);
		}

		readonly NodeModel _start;

		static readonly Color _defaultNodeColor = Color.DarkRed;
	}
}
