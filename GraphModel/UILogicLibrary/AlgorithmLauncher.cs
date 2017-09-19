using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GraphModelLibrary.Rewrite;

namespace UILogicLibrary {
	public static class AlgorithmLauncher {
		public static int EXECUTION_TIME_LIMIT = 1000;

		public static GraphModel RunAlgorithmA1(string algorithmPath, GraphModel inputGraph) {
			string inputText = GraphModelParser.SerializeA1(inputGraph);
			string outputText = null;

			ProcessStartInfo startInfo = new ProcessStartInfo(algorithmPath);
			startInfo.UseShellExecute = false;
			startInfo.RedirectStandardInput = true;
			startInfo.RedirectStandardOutput = true;

			using (Process algorithm = new Process()) {
				algorithm.StartInfo = startInfo;

				try {
					algorithm.Start();
					algorithm.StandardInput.Write(inputText);
					algorithm.StandardInput.Close();

					bool success = algorithm.WaitForExit(EXECUTION_TIME_LIMIT);
					if (success) {
						outputText = algorithm.StandardOutput.ReadToEnd();
					}
					else {
						throw new ExternalProcessException("Time limit exceeded");
					}
				}
				finally {
					if (algorithm != null) {
						try { algorithm.Kill(); }
						catch { }
					}
				}
			}

			return GraphModelParser.ParseA1(outputText);
		}
	}

	public class ExternalProcessException : Exception {
		public ExternalProcessException() { }

		public ExternalProcessException(string message) 
			: base(message) { }

		public ExternalProcessException(string message, Exception inner)
			: base(message, inner) { }
	}
}
