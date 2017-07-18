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

		public new NodeIndex CreateNode(NodeWeight weight) {
			NodeIndex nodeIndex = base.CreateNode(weight);
			this.ChangedEvent += weight.FireChangedEvent;
			return nodeIndex;
		}

		public new EdgeIndex CreateEdge(NodeIndex nodeFromIndex, NodeIndex nodeToIndex, EdgeWeight weight) {
			EdgeIndex edgeIndex = base.CreateEdge(nodeFromIndex, nodeToIndex, weight);
			this.ChangedEvent += weight.FireChangedEvent;
			return edgeIndex;
		}

		public new void DeleteNode(NodeIndex nodeIndex) {
			NodeWeight weight = this.GetNodeWeight(nodeIndex);
			base.DeleteNode(nodeIndex);
			this.ChangedEvent -= weight.FireChangedEvent;
		}

		public new void DeleteEdge(EdgeIndex edgeIndex) {
			EdgeWeight weight = this.GetEdgeWeight(edgeIndex);
			base.DeleteEdge(edgeIndex);
			this.ChangedEvent -= weight.FireChangedEvent;
		}

		public new void SetNodeWeight(NodeIndex nodeIndex, NodeWeight weight) {
			NodeWeight oldWeight = this.GetNodeWeight(nodeIndex);
			this.ChangedEvent -= oldWeight.FireChangedEvent;
			this.ChangedEvent += weight.FireChangedEvent;
			base.SetNodeWeight(nodeIndex, weight);
		}

		public new void SetEdgeWeight(EdgeIndex edgeIndex, EdgeWeight weight) {
			EdgeWeight oldWeight = this.GetEdgeWeight(edgeIndex);
			this.ChangedEvent -= oldWeight.FireChangedEvent;
			this.ChangedEvent += weight.FireChangedEvent;
			base.SetEdgeWeight(edgeIndex, weight);
		}

		public void Reindex() {
			throw new NotImplementedException();
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
			public Point Location {
				get {
					return _location;
				}
				set {
					_location = value;
					this.FireChangedEvent();
				}
			}

			public event Action ChangedEvent = () => { };
			public void FireChangedEvent() {
				this.ChangedEvent();
			}


			private static Color DEFAULT_COLOR = Color.Blue;
			private const string DEFAULT_VALUE = "";

			private Color _color;
			private string _value;
			private Point _location;
		}

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
