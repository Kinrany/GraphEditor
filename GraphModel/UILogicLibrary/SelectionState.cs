using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {
	public class SelectionState : EditToolState {
		public SelectionState(EditTool tool, Point start) : base(tool) {
			this._start = start;
		}

		public override void Draw(DrawingContext context) {
			Point mouse = context.MousePosition;
			Rectangle rect = SelectionRectangle(mouse);
			context.DrawRectangle(rect);
		}

		public override void MouseLeftDepressed(Point location) {
			Depressed(location);
		}
		public override void MouseLeftDepressed(NodeModel node) {
			Depressed(node.Weight.Location);
		}

		Point _start;

		void Depressed(Point point) {
			Rectangle rect = SelectionRectangle(point);
			if (EditTool.Keyboard.IsKeyDown(Keyboard.Key.Shift)) {
				EditTool.Selection.Add(rect);
			}
			else {
				EditTool.Selection.Add(rect);
			}
			CurrentState = new DefaultState(EditTool);
		}

		Rectangle SelectionRectangle(Point point) {
			int x = Math.Min(point.X, _start.X);
			int y = Math.Min(point.Y, _start.Y);
			int width = Math.Abs(point.X - _start.X);
			int height = Math.Abs(point.Y - _start.Y);
			return new Rectangle(x, y, width, height);
		}
	}
}
