
namespace BitbucketBackup
{
    internal interface IRepository
    {
        /// <summary>
        /// Message to display when pulling from the remote repository
        /// </summary>
        string PullingMessage { get; }

        /// <summary>
        /// Pulls from the remote repository to the local one.
        /// </summary>
        void Pull();
    }
}
