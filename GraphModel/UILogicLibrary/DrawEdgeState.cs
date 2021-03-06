﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {
	using NodeWeight = GraphModel.NodeWeight;
	using EdgeWeight = GraphModel.EdgeWeight;

	class DrawEdgeState : EditToolState {
		public DrawEdgeState(EditTool editTool, NodeModel start) : base(editTool) {
			this._start = start;
		}

		public override void Draw(DrawingContext context) {
			context.DrawArrow(_start.Weight.Location, context.MousePosition, EditTool.PickedColor);
		}

		public override void MouseLeftClick(Point location) {
			CurrentState = new DefaultState(EditTool);
		}

		public override void MouseLeftClick(NodeModel node) {
			EdgeModel oldEdge = EdgeModel.Between(_start, node);
			oldEdge?.Delete();

			EdgeModel edge = _start.AddOutgoingEdge(node);
			edge.Weight = EdgeWeight.DEFAULT.Update(colorId: EditTool.PickedColorId, value: "1");

			CurrentState = new DefaultState(EditTool);
		}

		public override void MouseRightClick(Point location) {
			CurrentState = new DefaultState(EditTool);
		}

		public override void MouseRightClick(NodeModel node) {
			CurrentState = new DefaultState(EditTool);
		}

		readonly NodeModel _start;
	}
}
