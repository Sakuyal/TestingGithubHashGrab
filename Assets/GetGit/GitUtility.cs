using System.Diagnostics;
using UnityEditor;
using Debug = UnityEngine.Debug;
public class GitUtility {
    private static string EnvironmentVariable {
        get {
            string sPath = System.Environment.GetEnvironmentVariable("Path");
            var result = sPath.Split(';');
            for (int i = 0; i < result.Length; i++) {
                if (result[i].Contains(@"Git\bin")) {
                    sPath = result[i];
                }
            }
            return sPath;
        }
    }

    [MenuItem("Assets/Test/Get Git Commit ID")]

    public static void GetCommitID() {
        string gitPath = System.IO.Path.Combine(EnvironmentVariable, "git.exe");
        Debug.LogFormat("Commit: {0}", gitPath);
        Process p = new Process();
        p.StartInfo.FileName = gitPath;
        p.StartInfo.Arguments = "rev-parse HEAD";
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.OutputDataReceived += OnOutputDataReceived;
        p.Start();
        p.BeginOutputReadLine();
        p.WaitForExit();
    }

    private static void OnOutputDataReceived(object sender, DataReceivedEventArgs e) {
        if (e != null && !string.IsNullOrEmpty(e.Data)) {
            Debug.Log(e.Data);
        }
    }
}