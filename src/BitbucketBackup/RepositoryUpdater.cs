using System;

namespace BitbucketBackup
{
    /// <summary>
    /// Clones or pulls/updates a given repository to the local disk.
    /// </summary>
    internal class RepositoryUpdater
    {
        private Config config;

        /// <summary>
        /// Creates a new RepositoryUpdater instance
        /// </summary>
        /// <param name="config">configuration settings</param>
        public RepositoryUpdater(Config config)
        {
            this.config = config;
        }

        /// <summary>
        /// Updates the local repository to the same revision as the remote one
        /// </summary>
        /// <param name="repoType">type of the repository (hg or git)</param>
        /// <param name="repoUri">URI to remote repository</param>
        /// <param name="localFolder">local destination for repository clone</param>
        public void Update(string repoType, Uri repoUri, string localFolder)
        {
            var uriWithAuth = this.BuildUriWithAuth(repoUri);

            var repo = RepositoryFactory.Create(repoType, uriWithAuth.ToString(), localFolder);
            
            if (repo.PullingMessage.Contains("{0}"))
            {
                Console.WriteLine(repo.PullingMessage, repoUri);
            }
            else
            {
                Console.WriteLine(repo.PullingMessage);
            }

            repo.Pull();
        }

        /// <summary>
        /// Inserts parameters for authentication into an URI
        /// </summary>
        /// <param name="uriWithoutAuth">Uri without authentification</param>
        /// <returns>Uri with authentification</returns>
        private Uri BuildUriWithAuth(Uri uriWithoutAuth)
        {
            return new Uri(uriWithoutAuth.ToString().Replace("://", string.Format("://{0}:{1}@", Uri.EscapeDataString(config.UserName), Uri.EscapeDataString(config.PassWord))));
        }
    }
}
