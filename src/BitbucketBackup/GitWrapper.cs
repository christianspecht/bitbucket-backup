using System.Diagnostics;

namespace BitbucketBackup
{
    /// <summary>
    /// Simple wrapper for Git (assumes that git.exe is in your %PATH%)
    /// </summary>
    internal class GitWrapper
    {
        private string folder;

        /// <summary>
        /// Creates a new GitWrapper instance.
        /// </summary>
        /// <param name="folder">The folder for the local repository.</param>
        public GitWrapper(string folder)
        {
            this.folder = folder;
        }

        /// <summary>
        /// Executes Git with the given command
        /// </summary>
        /// <param name="gitCommand">The command to execute, e.g. "init"</param>
        public void Execute(string gitCommand)
        {      
            var info = new ProcessStartInfo();
            info.FileName = "git.exe";
            info.Arguments = gitCommand;
            info.CreateNoWindow = true;
            info.WorkingDirectory = this.folder;
            info.UseShellExecute = false;

            var git = new Process();
            git.StartInfo = info;
            git.Start();
            git.WaitForExit();
            git.Close();      
        }
    }
}
