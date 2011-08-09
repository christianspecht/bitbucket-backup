using System;
using System.IO;
using System.Net;

namespace BitbucketBackup
{
    /// <summary>
    /// Helper class for making requests to the Bitbucket API
    /// </summary>
    internal class BitbucketRequest
    {
        /// <summary>
        /// Base URI for all API calls
        /// </summary>
        private Uri baseuri;

        /// <summary>
        /// Creates a new Bitbucket API request.
        /// </summary>
        public BitbucketRequest()
        {
            baseuri = new Uri("https://api.bitbucket.org/1.0/");
        }

        /// <summary>
        /// Executes the request.
        /// </summary>
        /// <param name="requestUri">relative path of the </param>
        /// <returns>Response (as JSON string)</returns>
        public string Execute(string requestUri)
        {
            var uri = new Uri(baseuri, requestUri);
            var request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                return reader.ReadToEnd();
            }
        }
    }
}
