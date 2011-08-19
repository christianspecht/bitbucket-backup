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
                Directory.CreateDirectory(this.localfolder);
            }

            var repo = new Repository(this.localfolder);

            if (!Directory.Exists(Path.Combine(this.localfolder,".hg")))
            {
                repo.Init();
            }

            Console.WriteLine(Resources.Pulling, this.repouri);
            repo.Pull(this.repouri.ToString(), new PullCommand().WithUpdate(false));
        }
    }
}
