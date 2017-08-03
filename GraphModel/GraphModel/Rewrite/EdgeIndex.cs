using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public struct EdgeIndex {
		public EdgeIndex(int value) {
			this.Value = value;
		}

		public static readonly EdgeIndex NaN = new EdgeIndex(-1);

		public readonly int Value;

		public static implicit operator int(EdgeIndex edgeIndex) {
			return edgeIndex.Value;
		}

		public override string ToString() {
			return this.Value.ToString();
		}
	}
}
