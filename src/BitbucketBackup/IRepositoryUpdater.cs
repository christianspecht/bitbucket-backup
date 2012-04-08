using System;

namespace BitbucketBackup
{
    /// <summary>
    /// Clones or pulls/updates a given repository to the local disk.
    /// </summary>
    internal interface IRepositoryUpdater
    {
        /// <summary>
        /// Updates the local repository to the same revision as the remote one
        /// </summary>
        /// <param name="repoType">type of the repository (hg or git)</param>
        /// <param name="repoUri">URI to remote repository</param>
        /// <param name="localFolder">local destination for repository clone</param>
        void Update(string repoType, Uri repoUri, string localFolder);
    }
}
