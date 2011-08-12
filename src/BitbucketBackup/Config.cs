using System;
using System.Configuration;
using System.Text;

namespace BitbucketBackup
{
    /// <summary>
    /// helper class for getting values out of the config file
    /// </summary>
    internal class Config
    {
        /// <summary>
        /// Bitbucket password
        /// </summary>
        private string password;

        /// <summary>
        /// Bitbucket username
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Folder on local machine where backups are saved
        /// </summary>
        public string BackupFolder { get; private set; }

        /// <summary>
        /// login credentials (Base64 encoded string)
        /// </summary>
        public string Credentials { get; private set; }

        public Config()
        {
            this.UserName = ConfigurationManager.AppSettings["User"];
            this.password = ConfigurationManager.AppSettings["Password"];
            this.BackupFolder = ConfigurationManager.AppSettings["BackupFolder"];
            this.Credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(this.UserName + ":" + this.password));
        }
    }
}
