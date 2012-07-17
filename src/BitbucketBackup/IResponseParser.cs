using System.Collections.Generic;

namespace BitbucketBackup
{
    /// <summary>
    /// Parses the JSON response from the Bitbucket API
    /// </summary>
    internal interface IResponseParser
    {
        /// <summary>
        /// Parses the response
        /// </summary>
        /// <param name="json">JSON response from the Bitbucket API</param>
        /// <returns>list of repository objects</returns>
        IEnumerable<BitbucketRepository> Parse(string response);
    }
}
