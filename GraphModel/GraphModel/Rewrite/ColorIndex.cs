using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public struct ColorId {
		public ColorId(int value) {
			this.Value = value;
		}

		public static readonly ColorId NaN = new ColorId(-1);

		public readonly int Value;

		public static implicit operator int(ColorId nodeIndex) {
			return nodeIndex.Value;
		}

		public override string ToString() {
			return this.Value.ToString();
		}
	}
}
