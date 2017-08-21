using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphModelLibrary.Rewrite {
	public struct NodeIndex {
		public NodeIndex(int value) {
			this.Value = value;
		}

		public static readonly NodeIndex NaN = new NodeIndex(-1);

		public readonly int Value;

		public static implicit operator int(NodeIndex nodeIndex) {
			return nodeIndex.Value;
		}

		public override string ToString() {
			return this.Value.ToString();
		}
	}
}
