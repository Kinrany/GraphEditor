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
	}
}
