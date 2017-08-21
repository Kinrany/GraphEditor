using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication {
	public static class GraphicsExtension {
		public static void DrawRectangle(this Graphics graphics, Pen pen, RectangleF rect) {
			graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}
	}
}
