using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {
	public class DefaultState : EditToolState {
		public DefaultState(EditTool tool) : base(tool) { }

		public override void MouseLeftPressed(Point location) {
			CurrentState = new SelectionState(EditTool, location);
		}

		public override void MouseLeftPressed(NodeModel node) {
			CurrentState = new DragState(EditTool, node);
		}

		public override void MouseLeftClick(Point location) {
			this.CreateNode(location);
		}

		public override void MouseLeftClick(NodeModel node) {
			CurrentState = new DrawEdgeState(EditTool, node);
		}

		public override void MouseRightClick(Point location) {
			EditTool.Selection.Clear();
		}

		public override void KeyPressed(Keyboard.Key key) {
			if (key == Keyboard.Key.Delete) {
				EditTool.Selection.Delete();
			}
		}

		private void CreateNode(Point location) {
			var nodeWeight = new GraphModel.NodeWeight();
			nodeWeight.Location = location;
			nodeWeight.Color = EditTool.PickedColor;

			NodeModel node = NodeModel.Create(EditTool.GraphView.Graph, nodeWeight);

			foreach (NodeModel other in NodeModel.Enumerate(EditTool.GraphView.Graph)) {
				if (node.Equals(other)) {
					continue;
				}

				var outgoingEdgeWeight = new GraphModel.EdgeWeight("0");
				node.AddOutgoingEdge(other, outgoingEdgeWeight);

				var incomingEdgeWeight = new GraphModel.EdgeWeight("0");
				node.AddIncomingEdge(other, incomingEdgeWeight);
			}
		}
	}
}
