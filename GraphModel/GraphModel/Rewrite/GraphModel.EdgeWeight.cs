using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public partial class GraphModel {
		public struct EdgeWeight {
			public EdgeWeight(Color color, string value) {
				_color = color;
				_value = value;
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

			public static readonly EdgeWeight DEFAULT = new EdgeWeight(Color.Black, "");
			
			public EdgeWeight Update(Color? color = null, string value = null) {
				return new EdgeWeight(color ?? _color, value ?? _value);
			}


			private Color _color;
			private string _value;
		}
	}
}