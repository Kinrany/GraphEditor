using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public partial class GraphModel {
		public class EdgeWeight {
			public EdgeWeight(string value = DEFAULT_VALUE)
				: this(DEFAULT_COLOR, value) { }
			public EdgeWeight(Color color, string value = DEFAULT_VALUE) {
				this.Color = color;
				this.Value = value;
			}

			public Color Color {
				get {
					return _color;
				}
				set {
					_color = value;
					this.FireChangedEvent();
				}
			}
			public string Value {
				get {
					return _value;
				}
				set {
					_value = value;
					this.FireChangedEvent();
				}
			}

			public event Action ChangedEvent = () => { };
			public void FireChangedEvent() {
				this.ChangedEvent();
			}


			private static Color DEFAULT_COLOR = Color.Gray;
			private const string DEFAULT_VALUE = "";

			private Color _color;
			private string _value;
		}
	}
}