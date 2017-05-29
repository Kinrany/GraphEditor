using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {
	public abstract class EditToolState {
		public EditToolState(EditTool editTool) {
			this._editTool = editTool;
		}

		public virtual void MouseLeftClick(Point location) { }
		public virtual void MouseLeftClick(NodeModel node) { }
		public virtual void MouseLeftPressed(Point location) { }
		public virtual void MouseLeftPressed(NodeModel node) { }
		public virtual void MouseLeftDepressed(Point location) { }
		public virtual void MouseLeftDepressed(NodeModel node) { }
		public virtual void MouseRightClick(Point location) { }
		public virtual void MouseRightClick(NodeModel node) { }
		public virtual void MouseMoved(Point location) { }
		public virtual void KeyPressed(Keyboard.Key key) { }
		public virtual void Draw(DrawingContext context) { }

		protected EditToolState CurrentState {
			get {
				return _editTool.State;
			}
			set {
				_editTool.State = value;
			}
		}
		protected EditTool EditTool {
			get {
				return _editTool;
			}
		}

		private Timer _timer;
		private EditTool _editTool;
	}


	public class EmptyState : EditToolState {
		public EmptyState(EditTool tool) : base(tool) { }
	}
}
