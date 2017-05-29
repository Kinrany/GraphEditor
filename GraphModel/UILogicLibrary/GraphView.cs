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
			this._graph = graph;
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
		

		GraphModel _graph;
		int _nodeRadius = 12;
		int _edgeWidth = 2;

		double distance(Point a, Point b) {
			int dx = a.X - b.X;
			int dy = a.Y - b.Y;
			return Math.Sqrt(dx * dx + dy * dy);
		}
	}
}
