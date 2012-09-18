using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BitbucketBackup
{
    /// <summary>
    /// Parses the JSON response from the Bitbucket API
    /// </summary>
    internal class ResponseParser : IResponseParser
    {
        /// <summary>
        /// Parses the response
        /// </summary>
        /// <param name="json">JSON response from the Bitbucket API</param>
        /// <returns>list of repository objects</returns>
        public IEnumerable<BitbucketRepository> Parse(string response)
        {
            var json = JObject.Parse(response);

            var repos =
                from r in json["repositories"].Children()
                select new BitbucketRepository { RepoName = (string)r["slug"], HasWiki = (bool)r["has_wiki"], Scm = (string)r["scm"] };

            return repos;
        }
    }
}
