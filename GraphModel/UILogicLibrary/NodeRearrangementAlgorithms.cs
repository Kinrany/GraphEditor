using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {
	public static class NodeRearrangementAlgorithms {
		public static void Circle(GraphModel graph) {
			Rectangle bounds = new Rectangle(80, 80, 320, 160);
			Point middle = new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
			int radius = Math.Min(bounds.Height, bounds.Width) * 4 / 5;

			int n = graph.NodeCount;
			int number = 0;
			foreach (NodeModel node in NodeModel.Enumerate(graph)) {
				double angle = 2 * Math.PI * (number * 1.0 / n);
				int dx = (int)Math.Round(radius * Math.Sin(angle));
				int dy = -(int)Math.Round(radius * Math.Cos(angle));
				node.Location = middle + new Size(dx, dy);
				number += 1;
			}
		}
	}
}
