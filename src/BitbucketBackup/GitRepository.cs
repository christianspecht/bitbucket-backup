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

            if (!Directory.Exists(Path.Combine(this.folder, ".git")))
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
            this.git.Execute("fetch " + this.remoteuri + " refs/heads/*:refs/heads/*");
        }
    }
}
