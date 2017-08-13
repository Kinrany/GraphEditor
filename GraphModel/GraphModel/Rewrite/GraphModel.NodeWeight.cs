using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public partial class GraphModel {
		public struct NodeWeight {
			public NodeWeight(string value = DEFAULT_VALUE) : this(DEFAULT_COLOR) { }
			public NodeWeight(Color color, string value = DEFAULT_VALUE) {
				_color = color;
				_value = value;
				_location = new Point();
			}

			public Color Color {
				get {
					return _color;
				}
				set {
					_color = value;
				}
			}
			public string Value {
				get {
					return _value;
				}
				set {
					_value = value;
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


			private static Color DEFAULT_COLOR = Color.Blue;
			private const string DEFAULT_VALUE = "";

			private Color _color;
			private string _value;
			private Point _location;
		}
	}
}
