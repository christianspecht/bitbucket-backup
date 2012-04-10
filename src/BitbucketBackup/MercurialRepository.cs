using System;
using System.IO;
using Mercurial;

namespace BitbucketBackup
{
    /// <summary>
    /// Creates and pulls from Mercurial repositories.
    /// </summary>
    internal class MercurialRepository : RepositoryBase
    {
        private Repository repo;
        private IConfig config;

        public MercurialRepository(string remoteUri, string folder, IConfig config) : base(remoteUri, folder)
        {
            this.config = config;
        }

        public override string PullingMessage
        {
            get { return Resources.PullingMercurial; }
        }

        protected override void Init()
        {
            this.repo = new Repository(this.folder);

            if (!Directory.Exists(Path.Combine(this.folder, ".hg")))
            {
                repo.Init();
            }
        }

        public override void Pull()
        {
            if (this.repo == null)
            {
                throw new InvalidOperationException("You need to call Init() first!");
            }

            try
            {
                this.repo.Pull(this.remoteuri, new PullCommand().WithUpdate(false).WithTimeout(this.config.PullTimeout));
            }
            catch (MercurialException ex)
            {
                // catch only timeouts, re-throw everything else
                if (ex.Message == "The executable did not complete within the allotted time")
                {
                    throw new ClientException(String.Format(Resources.PullTimeoutExceeded, this.config.PullTimeout), null);
                }
                
                throw;
            }
        }
    }
}
