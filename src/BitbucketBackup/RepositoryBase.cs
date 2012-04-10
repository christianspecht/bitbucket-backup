using System.IO;

namespace BitbucketBackup
{
    /// <summary>
    /// Abstract base class for repository implementations
    /// </summary>
    internal abstract class RepositoryBase : IRepository
    {
        protected string remoteuri;
        protected string folder;

        /// <summary>
        /// Creates a new repository instance
        /// </summary>
        /// <param name="remoteUri">The URI of the remote repository</param>
        /// <param name="folder">The folder where the local repository is (or will be)</param>
        public RepositoryBase(string remoteUri, string folder)
        {
            this.remoteuri = remoteUri;
            this.folder = folder;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            this.Init();
        }

        /// <summary>
        /// Creates a new repository in the folder
        /// </summary>
        /// <remarks>
        /// - check if the folder already is a repository
        /// - if not, create it
        /// </remarks>
        protected abstract void Init();

        /// <summary>
        /// Message to display when pulling from the remote repository
        /// </summary>
        public abstract string PullingMessage { get; }

        /// <summary>
        /// Pulls from the remote repository to the local one.
        /// </summary>
        public abstract void Pull();
    }
}
