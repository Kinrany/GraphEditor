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
			public ColorId ColorId {
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

			public NodeWeight Update(string name = null, ColorId? color = null, Point? location = null) {
				var weight = new NodeWeight(name ?? _name);
				weight._color = color ?? _color;
				weight._location = location ?? _location;
				return weight;
			}


			private static ColorId DEFAULT_COLOR = new ColorId(0);

			private string _name;
			private ColorId _color;
			private Point _location;
		}
	}
}
