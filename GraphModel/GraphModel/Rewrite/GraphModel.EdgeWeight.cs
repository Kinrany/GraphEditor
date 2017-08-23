using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public partial class GraphModel {
		public struct EdgeWeight {
			public EdgeWeight(ColorId colorId, string value) {
				_colorId = colorId;
				_value = value;
			}
			
			public ColorId ColorId {
				get {
					return _colorId;
				}
				set {
					_colorId = value;
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

			public static readonly EdgeWeight DEFAULT = new EdgeWeight(new ColorId(0), "");
			
			public EdgeWeight Update(ColorId? color = null, string value = null) {
				return new EdgeWeight(color ?? _colorId, value ?? _value);
			}


			private ColorId _colorId;
			private string _value;
		}
	}
}