using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace BitbucketBackup
{
    /// <summary>
    /// main execution class
    /// </summary>
    internal class BitbucketBackup : IBitbucketBackup
    {
        private IConfig config;
        private IBitbucketRequest request;
        private IRepositoryUpdater updater;
        private IResponseParser parser;

        public BitbucketBackup(IConfig config, IBitbucketRequest request, IRepositoryUpdater updater, IResponseParser parser)
        {
            this.config = config;
            this.request = request;
            this.updater = updater;
            this.parser = parser;
        }

        /// <summary>
        /// execute backup
        /// </summary>
        public void Execute()
        {
            int waitSeconds = 2;
            int waitInputSeconds = 10;

            try
            {
                Console.WriteLine(Resources.IntroHeadline, FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
                Console.WriteLine(Resources.WebLink);
                Console.WriteLine();

                bool enterConfig = false;

                if (this.config.IsComplete())
                {
                    Console.WriteLine(Resources.SettingsPrompt, waitInputSeconds);
                    Console.WriteLine();

                    for (int seconds = 0; seconds < waitInputSeconds; seconds++)
                    {
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo info = Console.ReadKey(true);
                            if (info.Key == ConsoleKey.Spacebar)
                            {
                                enterConfig = true;
                                break;
                            }
                        }

                        Thread.Sleep(1000);
                    }
                }

                if (enterConfig || !this.config.IsComplete())
                {
                    this.config.Input();
                    Console.WriteLine();
                }

                Console.WriteLine(Resources.IntroUser, this.config.UserName);
                if (this.config.UseTeam())
                    Console.WriteLine(Resources.IntroTeam, this.config.TeamName);
                Console.WriteLine(Resources.IntroFolder, this.config.BackupFolder);
                Console.WriteLine();
                Thread.Sleep(waitSeconds * 1000);

                string user = this.config.UseTeam() ? this.config.TeamName : this.config.UserName;

                string resource = String.Format("users/{0}", user);
                string response = this.request.Execute(resource);

                if (response == string.Empty)
                {
                    Console.WriteLine(Resources.NoResponse, resource);
                    Console.WriteLine();
                    Console.WriteLine(Resources.PressEnter);
                    Console.ReadLine();
                    return;
                }

                var repos = this.parser.Parse(response);

                var baseUri = new Uri(String.Format("https://bitbucket.org/{0}/", user));

                foreach (var repo in repos.OrderBy(r => r.RepoName))
                {
                    var repoUri = new Uri(baseUri, repo.RepoName);
                    string repoPath = Path.Combine(this.config.BackupFolder, repo.RepoName);

                    this.updater.Update(repo.Scm, repoUri, repoPath);

                    if (repo.HasWiki)
                    {
                        var wikiUri = new Uri(baseUri, repo.RepoName + "/wiki");
                        string wikiPath = Path.Combine(this.config.BackupFolder, repo.RepoName + "-wiki");

                        this.updater.Update(repo.Scm, wikiUri, wikiPath);
                    }
                }

                Console.WriteLine();
                Console.WriteLine(Resources.BackupCompleted);
                Thread.Sleep(waitSeconds * 1000);
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