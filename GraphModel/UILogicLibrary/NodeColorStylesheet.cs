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
				Color.FromArgb(  0,   0,   0),
				Color.FromArgb(255,   0,   0),
				Color.FromArgb(  0, 255,   0),
				Color.FromArgb(  0,   0, 255),
				Color.FromArgb(128, 128,   0),
				Color.FromArgb(128,   0, 128),
				Color.FromArgb(  0, 128, 128),
				Color.FromArgb(128,  64,  64),
				Color.FromArgb( 64, 128,  64),
				Color.FromArgb( 64,  64, 128),
				Color.FromArgb(192,  64,   0),
				Color.FromArgb(  0, 192,  64),
				Color.FromArgb( 64,   0, 192),
				Color.FromArgb(192,   0,  64),
				Color.FromArgb( 64, 192,   0),
				Color.FromArgb(  0,  64, 192)
			};

		private Dictionary<ColorId, Color> _idToColor;
		private Dictionary<Color, ColorId> _colorToId;
	}
}
