using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {


	/// <summary>
	/// Colors not present in the list are stored as Color.ToArgb() * 100
	/// </summary>
	public class NodeColorStylesheet {
		public NodeColorStylesheet() {
			_colorToId = new Dictionary<Color, ColorId>();
			_idToColor = new Dictionary<ColorId, Color>();

			for (int i = 0; i < _colors.Length; ++i) {
				ColorId id = new ColorId(i);
				Color color = _colors[i];

				_colorToId[color] = id;
				_idToColor[id] = color;
			}
		}

		public ReadOnlyCollection<Color> Colors {
			get {
				return Array.AsReadOnly(_colors);
			}
		}

		public ColorId ColorToId(Color color) {
			ColorId id;
			if (!_colorToId.TryGetValue(color, out id)) {
				id = new ColorId(color.ToArgb() * 100);
			}
			return id;
		}

		public Color IdToColor(ColorId id) {
			Color color;
			if (!_idToColor.TryGetValue(id, out color)) {
				color = Color.FromArgb(id.Value / 100);
			}
			return color;
		}


		private Color[] _colors = {
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

		private Dictionary<ColorId, Color> _idToColor;
		private Dictionary<Color, ColorId> _colorToId;
	}
}
