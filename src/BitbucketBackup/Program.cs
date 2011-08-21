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

            try
            {
                var config = new Config();

                Console.WriteLine(Resources.IntroHeadline);
                Console.WriteLine();
                Console.WriteLine(Resources.IntroUser, config.UserName);
                Console.WriteLine(Resources.IntroFolder, config.BackupFolder);
                Console.WriteLine();
                Thread.Sleep(sleepTime);

                var request = new BitbucketRequest(config);

                string resource = "users/" + config.UserName;
                string response = request.Execute(resource);

                if (response == string.Empty)
                {
                    Console.WriteLine(Resources.NoResponse, resource);
                    Console.ReadLine();
                    return;
                }

                var json = JObject.Parse(response);
                var repos =
                    from r in json["repositories"].Children()
                    select new { RepoName = (string)r["slug"], HasWiki = (bool)r["has_wiki"] };

                var baseUri = new Uri("https://bitbucket.org/" + config.UserName + "/");

                foreach (var repo in repos)
                {
                    var repoUri = new Uri(baseUri, repo.RepoName);
                    string repoPath = Path.Combine(config.BackupFolder, repo.RepoName);

                    var updater = new RepositoryUpdater(repoUri, repoPath, config);
                    updater.Update();

                    if (repo.HasWiki)
                    {
                        var wikiUri = new Uri(baseUri, repo.RepoName + "/wiki");
                        string wikiPath = Path.Combine(config.BackupFolder, repo.RepoName + "-wiki");

                        var wikiUpdater = new RepositoryUpdater(wikiUri, wikiPath, config);
                        wikiUpdater.Update();
                    }
                }

                Console.WriteLine();
                Console.WriteLine(Resources.BackupCompleted);
            }
            catch (ClientException ex)
            {
                Console.WriteLine(Resources.ClientExceptionHeadline);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Thread.Sleep(sleepTime);
            }
        }
    }
}