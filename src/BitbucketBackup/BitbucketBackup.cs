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
        private readonly ILogger logger;

        public BitbucketBackup(IConfig config, IBitbucketRequest request, IRepositoryUpdater updater, IResponseParser parser, ILogger logger)
        {
            this.config = config;
            this.request = request;
            this.updater = updater;
            this.parser = parser;
            this.logger = logger;
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
                logger.WriteLine(Resources.IntroHeadline, FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
                logger.WriteLine(Resources.WebLink);
                logger.WriteLine();

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

                logger.WriteLine(Resources.IntroUser, this.config.UserName);
                if (this.config.UseTeam())
                    logger.WriteLine(Resources.IntroTeam, this.config.TeamName);
                logger.WriteLine(Resources.IntroFolder, this.config.BackupFolder);
                logger.WriteLine();
                Thread.Sleep(waitSeconds * 1000);

                string user = this.config.UseTeam() ? this.config.TeamName : this.config.UserName;

                string resource = String.Format("users/{0}", user);
                string response = this.request.Execute(resource);

                if (response == string.Empty)
                {
                    logger.WriteLine(Resources.NoResponse, resource);
                    logger.WriteLine();
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

                logger.WriteLine();
                logger.WriteLine(Resources.BackupCompleted);
                Thread.Sleep(waitSeconds * 1000);
            }
            catch (ClientException ex)
            {
                logger.WriteLine(Resources.ClientExceptionHeadline);
                logger.WriteLine(ex.Message);
                logger.WriteLine();
                logger.WriteLine(Resources.PressEnter);
            }
        }
    }
}