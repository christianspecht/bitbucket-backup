using System;
using System.IO;

namespace BitbucketBackup
{
    /// <summary>
    /// Creates and pulls from Git repositories.
    /// </summary>
    internal class GitRepository : RepositoryBase
    {
        private GitWrapper git;

        public GitRepository(string remoteUri, string folder) : base(remoteUri, folder) { }

        protected override void Init()
        {
            this.git = new GitWrapper(folder);

            if (!File.Exists(Path.Combine(this.folder, "HEAD")))
            {
                this.git.Execute("init --bare");
            }
        }

        public override string PullingMessage
        {
            get { return Resources.PullingGit; }
        }

        public override void Pull()
        {
            this.git.Execute(String.Format("fetch --force --prune {0} refs/heads/*:refs/heads/* refs/tags/*:refs/tags/*", this.remoteuri));
        }
    }
}
