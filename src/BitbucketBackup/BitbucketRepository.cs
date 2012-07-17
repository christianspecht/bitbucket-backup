
namespace BitbucketBackup
{
    /// <summary>
    /// Represents one Bitbucket repository
    /// </summary>
    internal class BitbucketRepository
    {
        /// <summary>
        /// repository name on Bitbucket
        /// </summary>
        public string RepoName { get; set; }

        /// <summary>
        /// does the repo have a wiki?
        /// </summary>
        public bool HasWiki { get; set; }

        /// <summary>
        /// type of Scm
        /// </summary>
        public string Scm { get; set; }
    }
}
