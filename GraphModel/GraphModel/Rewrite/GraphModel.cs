using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public partial class GraphModel : WeightedGraph<GraphModel.NodeWeight, GraphModel.EdgeWeight> {

		public GraphModel() : base() { }

		public string Text {
			get {
				return _text;
			}
			set {
				if (value == null) {
					throw new InvalidOperationException("Text can't be null.");
				}

				_text = value;
			}
		}


		private string _text = "";
	}

	public partial class GraphModel { 
		public class NodeWeight {
			public NodeWeight(string value = DEFAULT_VALUE) : this(DEFAULT_COLOR) { }
			public NodeWeight(Color color, string value = DEFAULT_VALUE) {
				this.Color = color;
				this.Value = value;
				this.Location = new Point(0, 0);
			}

			public Color Color;
			public string Value;
			public Point Location;

			private static Color DEFAULT_COLOR = Color.Blue;
			private const string DEFAULT_VALUE = "";
		}

		public class EdgeWeight {
			public EdgeWeight(string value = DEFAULT_VALUE) 
				: this(DEFAULT_COLOR, value) { }
			public EdgeWeight(Color color, string value = DEFAULT_VALUE) {
				this.Color = color;
				this.Value = value;
			}

			public Color Color;
			public string Value;

			private static Color DEFAULT_COLOR = Color.Gray;
			private const string DEFAULT_VALUE = "";
		}
	}
}
