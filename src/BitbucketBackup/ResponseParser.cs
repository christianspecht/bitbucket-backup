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

            // If the authentication fails, the BB API returns only a subset of the repository information.
            // One of the missing things is the "has_wiki" property. So if it's missing, the password is probably wrong
            if (json.SelectToken("repositories[0].has_wiki") == null)
            {
                throw new ClientException(Resources.AuthenticationFailed, null);
            }

            var repos =
                from r in json["repositories"].Children()
                select new BitbucketRepository { RepoName = (string)r["slug"], HasWiki = (bool)r["has_wiki"], Scm = (string)r["scm"] };

            return repos;
        }
    }
}
