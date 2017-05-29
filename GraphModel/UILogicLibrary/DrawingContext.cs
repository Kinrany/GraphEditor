using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace UILogicLibrary {
	public class DrawingContext {

		public readonly Graphics Graphics;
		public readonly Point MousePosition;
		public Color DefaultColor = Color.Magenta;
		public float DefaultWidth = 1;

		public DrawingContext(Graphics g, Point mouse) {
			this.Graphics = g;
			this.MousePosition = mouse;
			_pen = new Pen(DefaultColor, DefaultWidth);
		}

		public Pen DefaultPen {
			get {
				if (_pen == null || _pen.Color != DefaultColor || _pen.Width != DefaultWidth) {
					_pen = new Pen(DefaultColor, DefaultWidth);
				}
				return _pen;
			}
			set {
				DefaultColor = value.Color;
				DefaultWidth = value.Width;
			}
		}
		public Brush DefaultBrush {
			get {
				if (_brush == null || _brush.Color != DefaultColor) {
					_brush = new SolidBrush(DefaultColor);
				}
				return _brush;
			}
		}

		public void DrawLine(Point a, Point b, Pen pen = null) {
			pen = pen ?? DefaultPen;
			Graphics.DrawLine(pen, a, b);
		}
		public void DrawRectangle(Rectangle rect, Pen pen = null) {
			pen = pen ?? DefaultPen;
			Graphics.DrawRectangle(pen, rect);
		}
		public void DrawCircle(Point centre, int radius, Pen pen = null) {
			pen = pen ?? DefaultPen;
			Rectangle rect = new Rectangle();
			rect.Location = new Point(centre.X - radius, centre.Y - radius);
			rect.Size = new Size(radius * 2, radius * 2);
			Graphics.DrawEllipse(pen, rect);
		}
		public void FillEllipse(Rectangle rect, Brush brush = null) {
			brush = brush ?? DefaultBrush;
			Graphics.FillEllipse(brush, rect);
		}
		public void FillCircle(Point centre, int radius, Brush brush = null) {
			brush = brush ?? DefaultBrush;
			int x = centre.X;
			int y = centre.Y;
			int r = radius;
			Graphics.FillEllipse(brush, centre.X - r, centre.Y - r, r * 2, r * 2);
		}
		public void DrawArrow(Point a, Point b, Color? color = null) {
			Color c = color ?? DefaultColor;
			Pen pen = new Pen(c, 2);

            GraphicsPath capPath = new GraphicsPath();
            //capPath.AddLine(-10, 0, 10, 0);
            //capPath.AddLine(-10, 0, 0, 10);
            //capPath.AddLine(0, 10, 10, 0);

            capPath.AddLine(-3, -12, 3, -12);
            capPath.AddLine(-3, -12, 0, -6);
            capPath.AddLine(0, -6, 3, -12);
            pen.CustomEndCap = new System.Drawing.Drawing2D.CustomLineCap(null, capPath);

            Graphics.DrawLine(pen, a, b);
        }

		Pen _pen;
		SolidBrush _brush;
	}
}
