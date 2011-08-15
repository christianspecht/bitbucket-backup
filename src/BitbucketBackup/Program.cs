using System;
using System.IO;
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
                return;
            }

            var json = JObject.Parse(response);
            var repos = json.SelectToken("repositories").Select(n => (string)n.SelectToken("slug")).ToList();

            var baseUri = new Uri("https://bitbucket.org/" + config.UserName + "/");

            foreach (string repoName in repos)
            {
                var repoUri = new Uri(baseUri, repoName);
                string repoPath = Path.Combine(config.BackupFolder, repoName);                

                var updater = new RepositoryUpdater(repoUri, repoPath);
                updater.Update();
            }

            Console.ReadLine();
        }
    }
}
