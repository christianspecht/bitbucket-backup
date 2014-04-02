using System.Net;
using RestSharp;

namespace BitbucketBackup
{
    /// <summary>
    /// Helper class for making requests to the Bitbucket API
    /// </summary>
    internal class BitbucketRequest : IBitbucketRequest
    {
        /// <summary>
        /// Base URL for all API calls
        /// </summary>
        private string baseurl;

        private IConfig config;

        /// <summary>
        /// Creates a new Bitbucket API request.
        /// </summary>
        /// <param name="config">Config object (for login credentials)</param>
        public BitbucketRequest(IConfig config)
        {
            this.baseurl = "https://bitbucket.org/api/1.0/";
            this.config = config;
        }

        /// <summary>
        /// Executes the request.
        /// </summary>
        /// <param name="requestUri">relative path of the </param>
        /// <returns>Response (as JSON string)</returns>
        public string Execute(string requestUri)
        {
            var client = new RestClient(this.baseurl);
            client.Authenticator = new HttpBasicAuthenticator(this.config.UserName, this.config.PassWord);
            var request = new RestRequest(requestUri);
            var response = client.Execute(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new ClientException(string.Format(Resources.InvalidUsername, this.config.UserName), null);
                case HttpStatusCode.Unauthorized:
                    throw new ClientException(Resources.AuthenticationFailed, null);
            }

            return response.Content;
            
        }
    }
}
