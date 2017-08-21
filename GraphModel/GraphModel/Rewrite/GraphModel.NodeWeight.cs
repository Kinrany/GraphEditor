using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public partial class GraphModel {
		public struct NodeWeight {
			public NodeWeight(Object name) {
				_name = name.ToString();
				_color = DEFAULT_COLOR;
				_location = new Point();
			}

			public string Name {
				get {
					Debug.Assert(!string.IsNullOrWhiteSpace(_name));
					return _name;
				}
				set {
					Debug.Assert(!string.IsNullOrWhiteSpace(value));
					_name = value;
				}
			}
			public Color Color {
				get {
					return _color;
				}
				set {
					_color = value;
				}
			}
			public Point Location {
				get {
					return _location;
				}
				set {
					_location = value;
				}
			}

			public NodeWeight Update(string name, Color? color, Point? location) {
				var weight = new NodeWeight(name ?? _name);
				weight._color = color ?? _color;
				weight._location = location ?? _location;
				return weight;
			}


			private static Color DEFAULT_COLOR = Color.Black;

			private string _name;
			private Color _color;
			private Point _location;
		}
	}
}
