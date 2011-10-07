using System;

namespace BitbucketBackup
{
    /// <summary>
    /// Clones or pulls/updates a given repository to the local disk.
    /// </summary>
    internal class RepositoryUpdater
    {
        private string repotype;
        private Uri repouri;
        private Uri repouriwithauth;
        private string localfolder;
        private Config config;

        /// <summary>
        /// Creates a new RepositoryUpdater instance
        /// </summary>
        /// <param name="repoType"
        /// <param name="repoUri">URI to remote repository</param>
        /// <param name="localFolder">local destination for repository clone</param>
        /// <param name="config">configuration settings</param>
        public RepositoryUpdater(string repoType, Uri repoUri, string localFolder, Config config)
        {
            this.repotype = repoType;
            this.repouri = repoUri;
            this.localfolder = localFolder;
            this.config = config;

            string uriWithAuth = repoUri.ToString().Replace("://", string.Format("://{0}:{1}@", config.UserName, config.PassWord));
            this.repouriwithauth = new Uri(uriWithAuth);
        }

        /// <summary>
        /// Updates the local repository to the same revision as the remote one
        /// </summary>
        public void Update()
        {
            var repo = RepositoryFactory.Create(this.repotype, this.repouriwithauth.ToString(), this.localfolder);
            
            if (repo.PullingMessage.Contains("{0}"))
            {
                Console.WriteLine(repo.PullingMessage, this.repouri);
            }
            else
            {
                Console.WriteLine(repo.PullingMessage);
            }

            repo.Pull();
        }
    }
}
