using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public partial class GraphModel : WeightedGraph<GraphModel.NodeWeight, GraphModel.EdgeWeight> {

		public GraphModel() : base() { }

		public string Text { get; set; }



		public class NodeWeight {
			public NodeWeight(string value = DEFAULT_VALUE) : this(DEFAULT_COLOR) { }
			public NodeWeight(Color color, string value = DEFAULT_VALUE) {
				this.Color = color;
				this.Value = value;
			}

			public Color Color;
			public string Value;

			private static Color DEFAULT_COLOR = Color.Blue;
			private const string DEFAULT_VALUE = "";
		}

		public class EdgeWeight {
			public EdgeWeight(string value = DEFAULT_VALUE) : this(DEFAULT_COLOR) { }
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
