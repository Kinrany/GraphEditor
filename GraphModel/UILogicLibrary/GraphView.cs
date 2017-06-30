using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {
	public interface IGraphView {
		int NodeRadius { get; set; }
	}

	public class GraphView : IGraphView {
		public GraphView(GraphModel graph) {
			this._graph = graph ?? new GraphModel();
			this.setLocations();
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

		public GraphModel Graph {
			get {
				return _graph;
			}
		}

		public Object FindClicked(Point p) {
			foreach (NodeModel node in NodeModel.Enumerate(_graph)) {
				if (distance(node.Weight.Location, p) < NodeRadius) {
					return node;
				}
			}
			return null;
		}
		public void Draw(DrawingContext context) {
			DrawEdges(context);
			DrawEdgeValues(context);
			DrawNodes(context);
			DrawNodeNumbers(context);
		}


		private GraphModel _graph;
		private int _nodeRadius = 12;
		private int _edgeWidth = 2;

		private double distance(Point a, Point b) {
			int dx = a.X - b.X;
			int dy = a.Y - b.Y;
			return Math.Sqrt(dx * dx + dy * dy);
		}

		private void setLocations() {
			Rectangle bounds = new Rectangle(80, 80, 320, 160);
			Point middle = new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
			int radius = Math.Min(bounds.Height, bounds.Width) * 4/5;

			int n = _graph.NodeCount;
			foreach (NodeModel node in NodeModel.Enumerate(_graph)) {
				double angle = 2*Math.PI * (node.Index * 1.0 / n);
				int dx = (int)Math.Round(radius * Math.Sin(angle));
				int dy = (int)Math.Round(radius * Math.Cos(angle));
				node.Weight.Location = middle + new Size(dx, dy);
			}
		}

		void DrawEdges(DrawingContext context) {
			if (this.Graph == null) {
				throw new InvalidOperationException("Can't draw edges without a graph");
			}

			foreach (EdgeModel edge in EdgeModel.Enumerate(this.Graph)) {
				Point pointFrom = edge.NodeFrom.Weight.Location;
				Point pointTo = edge.NodeTo.Weight.Location;
				Color color = edge.Weight.Color;
				string value = edge.Weight.Value;

				bool shouldDraw = value != "0" && value != "";

				if (shouldDraw) {
					context.DrawArrow(pointFrom, pointTo, color);
				}
			}
		}
		void DrawNodes(DrawingContext context) {
			if (this.Graph == null) {
				throw new InvalidOperationException("Can't draw nodes without a graph");
			}

			foreach (NodeModel node in NodeModel.Enumerate(this.Graph)) {
				Point point = node.Weight.Location;
				Color color = node.Weight.Color;
				context.FillCircle(point, this.NodeRadius, new SolidBrush(color));
			}
		}
		void DrawNodeNumbers(DrawingContext context) {
			foreach (NodeModel node in NodeModel.Enumerate(this.Graph)) {
				context.DrawText(node.Index.ToString(), node.Weight.Location, Brushes.Black);
			}
		}
		void DrawEdgeValues(DrawingContext context) {
			foreach (EdgeModel edge in EdgeModel.Enumerate(this.Graph)) {
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
