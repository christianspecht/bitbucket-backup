using System;
using System.IO;
using Mercurial;

namespace BitbucketBackup
{
    /// <summary>
    /// Clones or pulls/updates a given repository to the local disk.
    /// </summary>
    internal class RepositoryUpdater
    {
        /// <summary>
        /// URI to remote repository
        /// </summary>
        private Uri repouri;

        /// <summary>
        /// local destination for repository clone
        /// </summary>
        private string localfolder;

        /// <summary>
        /// Creates a new RepositoryUpdater instance
        /// </summary>
        /// <param name="repoUri">URI to remote repository</param>
        /// <param name="destinationFolder">local destination for repository clone</param>
        public RepositoryUpdater(Uri repoUri, string localFolder)
        {
            this.repouri = repoUri;
            this.localfolder = localFolder;
        }

        /// <summary>
        /// Updates the local repository to the same revision as the remote one
        /// </summary>
        public void Update()
        {
            if (!Directory.Exists(this.localfolder))
            {
                // repo doesn't exist in destination folder --> clone
                Console.WriteLine("Cloning: {0}", this.repouri);
                Directory.CreateDirectory(this.localfolder);
                var repo = new Repository(this.localfolder);
                repo.Clone(this.repouri.ToString(), new CloneCommand().WithUpdate(false));
            }
            else
            {
                // repo already exists --> just pull
                throw new NotImplementedException("TODO");
            }
        }
    }
}
