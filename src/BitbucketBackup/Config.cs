using System;
using BitbucketBackup.Properties;

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
        public string PassWord
        {
            get
            {
                return Settings.Default.PassWord;
            }
            private set
            {
                Settings.Default.PassWord = value;
                Settings.Default.Save();
            }

        }

        /// <summary>
        /// Bitbucket username
        /// </summary>
        public string UserName
        {
            get
            {
                return Settings.Default.UserName;
            }
            private set
            {
                Settings.Default.UserName = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Folder on local machine where backups are saved
        /// </summary>
        public string BackupFolder
        {
            get
            {
                return Settings.Default.BackupFolder;
            }
            private set
            {
                Settings.Default.BackupFolder = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Checks if the config is complete (if none of the settings is empty)
        /// </summary>
        public bool IsComplete
        {
            get
            {
                return this.UserName != string.Empty
                    && this.PassWord != string.Empty
                    && this.BackupFolder != string.Empty;
            }
        }

        /// <summary>
        /// Asks for the config settings
        /// </summary>
        public void Input()
        {
            Console.WriteLine(Resources.InputUser);
            this.UserName = Console.ReadLine();

            Console.WriteLine(Resources.InputPassword);
            this.PassWord = Console.ReadLine();

            Console.WriteLine(Resources.InputBackupFolder);
            this.BackupFolder = Console.ReadLine();
        }
    }
}
