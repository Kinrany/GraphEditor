using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {



	public interface IReadonlyNodeColorStylesheet {
		ColorId ColorToId(Color color);
		Color IdToColor(ColorId id);
	}

	public class NodeColorStylesheet : IReadonlyNodeColorStylesheet {
		public NodeColorStylesheet() {
			_colorToId = new Dictionary<Color, ColorId>();
			_idToColor = new Dictionary<ColorId, Color>();

			Color[] colors = {
				Color.Magenta,
				Color.Blue,
				Color.Red,
				Color.Green,
				Color.Black,
				Color.White,
				Color.Purple,
				Color.Orange,
				Color.Yellow,
				Color.Pink,
				Color.Turquoise,
				Color.Gray,
				Color.Teal,
				Color.DarkBlue,
				Color.Violet,
				Color.Brown,
				Color.Aquamarine
			};
			
			for (int i = 0; i < colors.Length; ++i) {
				ColorId id = new ColorId(i);
				Color color = colors[i];

				_colorToId[color] = id;
				_idToColor[id] = color;
			}
		}

		public ColorId ColorToId(Color color) {
			return _colorToId[color];
		}

		public Color IdToColor(ColorId id) {
			return _idToColor[id];
		}


		private Dictionary<ColorId, Color> _idToColor;
		private Dictionary<Color, ColorId> _colorToId;
	}
}
