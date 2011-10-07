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
                    Console.WriteLine();
                    Console.WriteLine(Resources.PressEnter);
                    Console.ReadLine();
                    return;
                }

                var json = JObject.Parse(response);

                // If the authentication fails, the BB API returns only a subset of the repository information.
                // One of the missing things is the "has_wiki" property. So if it's missing, the password is probably wrong
                if (json.SelectToken("repositories[0].has_wiki") == null)
                {                    
                    throw new ClientException(Resources.AuthenticationFailed, null);
                }

                var repos =
                    from r in json["repositories"].Children()
                    select new { RepoName = (string)r["slug"], HasWiki = (bool)r["has_wiki"], Scm = (string)r["scm"] };

                var baseUri = new Uri("https://bitbucket.org/" + config.UserName + "/");

                foreach (var repo in repos)
                {
                    if (repo.Scm == "hg")
                    {
                        var repoUri = new Uri(baseUri, repo.RepoName);
                        string repoPath = Path.Combine(config.BackupFolder, repo.RepoName);

                        var updater = new RepositoryUpdater(repo.Scm, repoUri, repoPath, config);
                        updater.Update();

                        if (repo.HasWiki)
                        {
                            var wikiUri = new Uri(baseUri, repo.RepoName + "/wiki");
                            string wikiPath = Path.Combine(config.BackupFolder, repo.RepoName + "-wiki");

                            var wikiUpdater = new RepositoryUpdater(repo.Scm, wikiUri, wikiPath, config);
                            wikiUpdater.Update();
                        }
                    }
                }

                Console.WriteLine();
                Console.WriteLine(Resources.BackupCompleted);
                Thread.Sleep(sleepTime);
            }
            catch (ClientException ex)
            {
                Console.WriteLine(Resources.ClientExceptionHeadline);
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine(Resources.PressEnter);
                Console.ReadLine();
            }
        }
    }
}