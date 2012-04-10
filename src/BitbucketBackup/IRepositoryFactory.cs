
namespace BitbucketBackup
{
    internal interface IRepositoryFactory
    {
        /// <summary>
        /// Creates a new instance of the given repository type
        /// </summary>
        /// <param name="repoType">The desired repository type</param>
        /// <param name="remoteUri">The URI of the remote repository</param>
        /// <param name="localFolder">The folder where the local repository is (or will be)</param>
        /// <returns>new repository instance</returns>
        IRepository Create(string repoType, string remoteUri, string localFolder);
    }
}
