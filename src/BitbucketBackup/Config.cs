using System.Configuration;

namespace BitbucketBackup
{
    internal class Config
    {
        public string UserName { get; private set; }
        public string PassWord { get; private set; }
        public string BackupFolder { get; private set; }

        public Config()
        {
            this.UserName = ConfigurationManager.AppSettings["User"];
            this.PassWord = ConfigurationManager.AppSettings["Password"];
            this.BackupFolder = ConfigurationManager.AppSettings["BackupFolder"];
        }
    }
}
