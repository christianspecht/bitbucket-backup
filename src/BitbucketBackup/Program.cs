using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BitbucketBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Config();
            var request = new BitbucketRequest(config.Credentials);

            string resource = "users/" + config.UserName;
            string response = request.Execute(resource);

            if (response == string.Empty)
            {
                Console.WriteLine("Bitbucket API didn't return a response: " + resource);
                Console.ReadLine();
            }

            var json = JObject.Parse(response);
            var repos = json.SelectToken("repositories").Select(n => (string)n.SelectToken("slug")).ToList();

            foreach (string repo in repos)
            {
                Console.WriteLine(repo);
            }

            Console.ReadLine();
        }
    }
}
