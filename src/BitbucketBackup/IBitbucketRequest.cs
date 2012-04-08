
namespace BitbucketBackup
{
    /// <summary>
    /// Helper class for making requests to the Bitbucket API
    /// </summary>
    internal interface IBitbucketRequest
    {
        /// <summary>
        /// Executes the request.
        /// </summary>
        /// <param name="requestUri">relative path of the </param>
        /// <returns>Response (as JSON string)</returns>
        string Execute(string requestUri);
    }
}
