using System.Diagnostics;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace _GameAssets.Scripts.Editor {
	public class AutoVersionUpdateBuildPreprocessor : IPreprocessBuildWithReport {
		public int callbackOrder => 0;
		
		public void OnPreprocessBuild(BuildReport report) {
			UpdateBuildVersion();
		}

		// automatically sets current build commit as a app version. 
		static void UpdateBuildVersion() {
			// run git log --pretty=format:'%h' -n 1
			var process = new Process {
				StartInfo = new ProcessStartInfo {
					FileName               = "git",
					Arguments              = "log --pretty=format:'%h' -n 1",
					UseShellExecute        = false,
					RedirectStandardOutput = true,
					CreateNoWindow         = true,
				},
			};
			process.Start();
			// read result
			var commitHash = process.StandardOutput.ReadToEnd().Trim();
			process.WaitForExit();
			// set commit hash to build options
			PlayerSettings.bundleVersion = commitHash.Replace("'", string.Empty);
		}
	}
}