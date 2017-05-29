using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphModelLibrary;
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

			Graph graph = _graphModel.Graph;
			foreach (var pair in graph.Indexed()) {
				int index = (int)pair.Key;
				NodeModel node = (NodeModel)pair.Value;
				Point point = node.Location;
				Color color = node.Color;
				context.FillCircle(point, GraphView.NodeRadius, new SolidBrush(color));
			}
		}

		private void drawEdges(DrawingContext context) {
			if (_graphModel == null) {
				throw new InvalidOperationException("Can't draw edges without a graph");
			}

			RectangleF bounds = context.Graphics.VisibleClipBounds;
			PointF middle = new PointF(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);

			Graph graph = _graphModel.Graph;
			foreach (NodeModel node in graph) {
				foreach (EdgeModel edge in node.GetOutgoingEdges()) {
					NodeModel node2 = edge.To as NodeModel;
					Color color = edge.Color;
					context.DrawArrow(node.Location, node2.Location, color);
				}
			}
		}
	}
}
