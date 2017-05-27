using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphModelLibrary.Rewrite;
using System.IO;

namespace UnitTestProject {
	[TestClass]
	public class GraphModelLibraryUnitTest {
		[TestMethod]
		public void Parsing1() {
			string text =
@"5
0 1 0 1 1
1 0 0 1 1
1 0 0 0 1
1 0 1 0 1
1 1 1 1 0";

			GraphModel model = GraphModelParser.ParseA1(text);
		}

		[TestMethod]
		public void Parsing2() {
			string text =
@"3
0 2 3
-1 0 0
5 42 0
Node colors:
1 0 3
Text:
Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Morbi elementum lorem et libero bibendum, ac egestas urna accumsan.";

			GraphModel model = GraphModelParser.ParseA1(text);
		}

		[TestMethod]
		public void Parsing3() {
			string text =
@"4
0 2 3 100
-1 0 0 -1
5 42 0 24
1 1 -10 0
Edge colors:
0 1 5
0 2 3
-1
Node colors:
1 0 3
Text:
Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Morbi elementum lorem et libero bibendum, ac egestas urna accumsan.";

			GraphModel model = GraphModelParser.ParseA1(text);
		}

		[TestMethod]
		public void Loading1() {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-1.txt");
			GraphModel model = GraphModelParser.Load(path);
		}

		[TestMethod]
		public void Loading2() {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-2.txt");
			GraphModel model = GraphModelParser.Load(path);
		}

		[TestMethod]
		public void Loading3() {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-3.txt");
			GraphModel model = GraphModelParser.Load(path);
		}

		[TestMethod]
		public void Serializing1() {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-3.txt");
			GraphModel model = GraphModelParser.Load(path);
			string[] result = GraphModelParser.SerializeA1(model);
		}

		[TestMethod]
		public void Saving1() {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Examples", @"exampleA1-3.txt");
			GraphModel model = GraphModelParser.Load(path);
			string savePath = @"~tempfile.txt";
			GraphModelParser.Save(model, savePath);
			try {
				GraphModel model2 = GraphModelParser.Load(savePath);
			}
			finally {
				File.Delete(savePath);
			}
		}
	}
}
