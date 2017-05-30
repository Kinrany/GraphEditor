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

		public new int CreateNode(NodeWeight weight) {
			int nodeIndex = base.CreateNode(weight);
			this.ChangedEvent += weight.FireChangedEvent;
			return nodeIndex;
		}

		public new int CreateEdge(int nodeFromIndex, int nodeToIndex, EdgeWeight weight) {
			int edgeIndex = base.CreateEdge(nodeFromIndex, nodeToIndex, weight);
			this.ChangedEvent += weight.FireChangedEvent;
			return edgeIndex;
		}

		public new void DeleteNode(int nodeIndex) {
			NodeWeight weight = this.GetNodeWeight(nodeIndex);
			this.ChangedEvent -= weight.FireChangedEvent;
			base.DeleteNode(nodeIndex);
		}

		public new void DeleteEdge(int edgeIndex) {
			EdgeWeight weight = this.GetEdgeWeight(edgeIndex);
			this.ChangedEvent -= weight.FireChangedEvent;
			base.DeleteEdge(edgeIndex);
		}

		public new void SetNodeWeight(int nodeIndex, NodeWeight weight) {
			NodeWeight oldWeight = this.GetNodeWeight(nodeIndex);
			this.ChangedEvent -= oldWeight.FireChangedEvent;
			this.ChangedEvent += weight.FireChangedEvent;
			base.SetNodeWeight(nodeIndex, weight);
		}

		public new void SetEdgeWeight(int edgeIndex, EdgeWeight weight) {
			EdgeWeight oldWeight = this.GetEdgeWeight(edgeIndex);
			this.ChangedEvent -= oldWeight.FireChangedEvent;
			this.ChangedEvent += weight.FireChangedEvent;
			base.SetEdgeWeight(edgeIndex, weight);
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

			public event Action ChangedEvent = () => { };
			public void FireChangedEvent() {
				this.ChangedEvent();
			}

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

			public event Action ChangedEvent = () => { };
			public void FireChangedEvent() {
				this.ChangedEvent();
			}

			private static Color DEFAULT_COLOR = Color.Gray;
			private const string DEFAULT_VALUE = "";
		}
	}
}
