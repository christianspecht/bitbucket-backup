using System.Configuration;

namespace BitbucketBackup
{
    /// <summary>
    /// helper class for getting values out of the config file
    /// </summary>
    internal class Config
    {
        /// <summary>
        /// Bitbucket username
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Bitbucket password
        /// </summary>
        private string password;

        /// <summary>
        /// Folder on local machine where backups are saved
        /// </summary>
        public string BackupFolder { get; private set; }

        public Config()
        {
            this.UserName = ConfigurationManager.AppSettings["User"];
            this.password = ConfigurationManager.AppSettings["Password"];
            this.BackupFolder = ConfigurationManager.AppSettings["BackupFolder"];
        }
    }
}
