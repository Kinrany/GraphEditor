﻿using System;
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
			NodeModel node = NodeModel.Create(EditTool.Graph);
			var nodeWeight = new GraphModel.NodeWeight(node.Index);
			nodeWeight.Location = location;
			nodeWeight.Color = EditTool.PickedColor;
			node.Weight = nodeWeight;

			foreach (NodeModel other in NodeModel.Enumerate(EditTool.Graph)) {
				if (node.Equals(other)) {
					continue;
				}

				EdgeModel outgoingEdge = node.AddOutgoingEdge(other);
				var outgoingEdgeWeight = new GraphModel.EdgeWeight(outgoingEdge.Index);
				outgoingEdgeWeight.Value = "0";
				outgoingEdge.Weight = outgoingEdgeWeight;

				EdgeModel incomingEdge = node.AddIncomingEdge(other);
				var incomingEdgeWeight = new GraphModel.EdgeWeight(incomingEdge.Index);
				incomingEdgeWeight.Value = "0";
				incomingEdge.Weight = incomingEdgeWeight;
			}
		}
	}
}
