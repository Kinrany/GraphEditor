using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public partial class GraphModel {
		public struct EdgeWeight {
			public EdgeWeight(Object name) {
				_name = name.ToString();
				_color = DEFAULT_COLOR;
				_value = "";
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
			public string Value {
				get {
					return _value;
				}
				set {
					_value = value;
				}
			}

			private static Color DEFAULT_COLOR = Color.Black;

			private string _name;
			private Color _color;
			private string _value;
		}
	}
}