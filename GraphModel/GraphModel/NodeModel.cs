using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary {
	public class NodeModel : Node {
		public NodeModel() :this(_defaultLocation, _defaultColor) { }
		public NodeModel(Color color) :this(_defaultLocation, _defaultColor) { }
		public NodeModel(Point location) : this(location, _defaultColor) { }
		public NodeModel(Point location, Color color) : base() {
			this.Color = color;
			this.Location = location;
		}

		public Color Color {
			get; set;
		}
		public Point Location {
			get; set;
		}

		static readonly Color _defaultColor = Color.DarkBlue;
		static readonly Point _defaultLocation = Point.Empty;
	}
}
