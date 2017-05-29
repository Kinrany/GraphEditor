using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphModelLibrary.Rewrite;
using UILogicLibrary;
using ExtensionMethods;

namespace WindowsFormsApplication {
	partial class Form1 {
		private void drawGraph(DrawingContext context) {
			if (_graphModel != null) {
				drawEdges(context);
				drawNodes(context);
			}
		}

		private void drawNodes(DrawingContext context) {
			if (_graphModel == null) {
				throw new InvalidOperationException("Can't draw nodes without a graph");
			}

			RectangleF bounds = context.Graphics.VisibleClipBounds;
			PointF middle = new PointF(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
			
			foreach (NodeModel node in NodeModel.Enumerate(_graphModel)) {
				Point point = node.Weight.Location;
				Color color = node.Weight.Color;
				context.FillCircle(point, GraphView.NodeRadius, new SolidBrush(color));
			}
		}

		private void drawEdges(DrawingContext context) {
			if (_graphModel == null) {
				throw new InvalidOperationException("Can't draw edges without a graph");
			}

			RectangleF bounds = context.Graphics.VisibleClipBounds;
			PointF middle = new PointF(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
			
			foreach (NodeModel nodeFrom in NodeModel.Enumerate(_graphModel)) {
				foreach (EdgeModel edge in nodeFrom.OutgoingEnumerator) {
					NodeModel nodeTo = edge.NodeTo;
					Color color = edge.Weight.Color;
					context.DrawArrow(nodeFrom.Weight.Location, nodeTo.Weight.Location, color);
				}
			}
		}
	}
}
