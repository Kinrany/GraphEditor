using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UILogicLibrary {
	public abstract class Keyboard {
		public abstract bool IsKeyDown(Key key);

		public event KeyPressedDelegate KeyPressed = (Key _) => { };

		public delegate void KeyPressedDelegate(Key key);

		public enum Key {
			Shift,
			Delete
		}

		protected void CallKeyPressed(Key key) {
			if (KeyPressed != null) {
				KeyPressed(key);
			}
		}
	}
}
