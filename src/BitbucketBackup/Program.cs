using System;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace BitbucketBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            int sleepTime = 2000;

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

            Console.WriteLine("Bitbucket Backup");
            Console.WriteLine();
            Console.WriteLine("Bitbucket user: {0}", config.UserName);
            Console.WriteLine("Local backup folder: {0}", config.BackupFolder);
            Console.WriteLine();
            Thread.Sleep(sleepTime);

            var json = JObject.Parse(response);
            var repos =
                from r in json["repositories"].Children()
                select new { RepoName = (string)r["slug"], HasWiki = (bool)r["has_wiki"] };

            var baseUri = new Uri("https://bitbucket.org/" + config.UserName + "/");

            foreach (var repo in repos)
            {
                var repoUri = new Uri(baseUri, repo.RepoName);
                string repoPath = Path.Combine(config.BackupFolder, repo.RepoName);                

                var updater = new RepositoryUpdater(repoUri, repoPath);
                updater.Update();

                if (repo.HasWiki)
                {
                    var wikiUri = new Uri(baseUri, repo.RepoName + "/wiki");
                    string wikiPath = Path.Combine(config.BackupFolder, repo.RepoName + "-wiki");

                    var wikiUpdater = new RepositoryUpdater(wikiUri, wikiPath);
                    wikiUpdater.Update();
                }
            }

            Console.WriteLine();
            Console.WriteLine("Backup completed!");
            Thread.Sleep(sleepTime);
        }
    }
}
