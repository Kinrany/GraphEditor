﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {
	class DrawEdgeState : EditToolState {
		public DrawEdgeState(EditTool editTool, NodeModel start) : base(editTool) {
			this._start = start;
		}

		public override void Draw(DrawingContext context) {
			context.DrawArrow(_start.Weight.Location, context.MousePosition);
		}

		public override void MouseLeftClick(Point location) {
			CurrentState = new DefaultState(EditTool);
		}

		public override void MouseLeftClick(NodeModel node) {
			EdgeModel edge = _start.AddOutgoingEdge(node);
			edge.Weight.Color = _defaultNodeColor;

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
