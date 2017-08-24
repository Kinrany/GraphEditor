using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {
	public class GraphView {

		public GraphView() {
			_stylesheet = new NodeColorStylesheet();
		}

		public int NodeRadius {
			get {
				return _nodeRadius;
			}
			set {
				_nodeRadius = value;
			}
		}
		public int EdgeWidth {
			get {
				return _edgeWidth;
			}
			set {
				_edgeWidth = value;
			}
		}
		public NodeColorStylesheet Stylesheet {
			get {
				return _stylesheet;
			}
		}

		public Object FindClicked(GraphModel graph, Point p) {
			foreach (NodeModel node in NodeModel.Enumerate(graph)) {
				if (distance(node.Weight.Location, p) < NodeRadius) {
					return node;
				}
			}
			return null;
		}
		public void Draw(GraphModel graph, DrawingContext context) {
			DrawEdges(graph, context);
			DrawEdgeValues(graph, context);
			DrawNodes(graph, context);
			DrawNodeNumbers(graph, context);
		}

		
		private int _nodeRadius = 12;
		private int _edgeWidth = 2;
		private NodeColorStylesheet _stylesheet;

		private double distance(Point a, Point b) {
			int dx = a.X - b.X;
			int dy = a.Y - b.Y;
			return Math.Sqrt(dx * dx + dy * dy);
		}

		void DrawEdges(GraphModel graph, DrawingContext context) {
			if (graph == null) {
				throw new InvalidOperationException("Can't draw edges without a graph");
			}

			foreach (EdgeModel edge in EdgeModel.Enumerate(graph)) {
				Point pointFrom = edge.NodeFrom.Weight.Location;
				Point pointTo = edge.NodeTo.Weight.Location;

				ColorId colorId = edge.Weight.ColorId;
				Color color = _stylesheet.IdToColor(colorId);

				string value = edge.Weight.Value;

				bool shouldDraw = value != "0" && value != "";

				if (shouldDraw) {
					context.DrawArrow(pointFrom, pointTo, color);
				}
			}
		}
		void DrawNodes(GraphModel graph, DrawingContext context) {
			if (graph == null) {
				throw new InvalidOperationException("Can't draw nodes without a graph");
			}

			foreach (NodeModel node in NodeModel.Enumerate(graph)) {
				Point point = node.Weight.Location;

				ColorId colorId = node.Weight.ColorId;
				Color color = _stylesheet.IdToColor(colorId);

				context.FillCircle(point, this.NodeRadius, new SolidBrush(color));
			}
		}
		void DrawNodeNumbers(GraphModel graph, DrawingContext context) {
			foreach (NodeModel node in NodeModel.Enumerate(graph)) {
				context.DrawText(node.Name, node.Weight.Location, Brushes.Black);
			}
		}
		void DrawEdgeValues(GraphModel graph, DrawingContext context) {
			foreach (EdgeModel edge in EdgeModel.Enumerate(graph)) {
				Point nodeFromLocation = edge.NodeFrom.Weight.Location;
				Point nodeToLocation = edge.NodeTo.Weight.Location;
				PointF drawPosition = new PointF(
					nodeFromLocation.X / 2.0f + nodeToLocation.X / 2.0f,
					nodeFromLocation.Y / 2.0f + nodeToLocation.Y / 2.0f);
				string value = edge.Weight.Value;
				if (value != "" && value != "0") {
					context.DrawText(value, drawPosition);
				}
			}
		}
	}
}
