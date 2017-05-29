using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary {
	public class EdgeModel : Edge {
		public EdgeModel(Node from, Node to, Color color) : base(from, to) {
			this._color = color;
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

		Color _color;
		string _value = "";
	}
}
