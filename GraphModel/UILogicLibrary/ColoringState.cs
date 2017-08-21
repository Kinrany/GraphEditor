using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {
	public class ColoringState : EditToolState {
		public ColoringState(EditTool editTool) : base(editTool) { }

		public override void MouseLeftClick(NodeModel node) {
			node.Color = EditTool.PickedColor;
		}
	}
}