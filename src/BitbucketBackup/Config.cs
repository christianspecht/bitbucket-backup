using System;
using System.IO;
using BitbucketBackup.Properties;

namespace BitbucketBackup
{
    /// <summary>
    /// helper class for getting values out of the config file
    /// </summary>
    internal class Config : IConfig
    {
        /// <summary>
        /// default timeout for pulling
        /// </summary>
        const int DefaultPullTimeout = 60;

        /// <summary>
        /// Bitbucket password
        /// </summary>
        public string PassWord
        {
            get
            {
                if (String.IsNullOrEmpty(Settings.Default.PassWord))
                {
                    return Settings.Default.PassWord;
                }

                return StringEncryptor.Decrypt(Settings.Default.PassWord);
            }
            private set
            {
                if (value == string.Empty)
                {
                    throw new ClientException(Resources.InputPasswordInvalid, null);
                }

                Settings.Default.PassWord = StringEncryptor.Encrypt(value);
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
                if (value == string.Empty)
                {
                    throw new ClientException(Resources.InputUserInvalid, null);
                }

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
                if (value == string.Empty || !Directory.Exists(value))
                {
                    throw new ClientException(Resources.InputBackupFolderInvalid, null);
                }

                Settings.Default.BackupFolder = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Timeout for pulling (only for Mercurial at the moment)
        /// </summary>
        public int PullTimeout
        {
            get
            {
                return Settings.Default.PullTimeout;
            }
        }

        /// <summary>
        /// Timeout for pulling (setting only)
        /// </summary>
        /// <remarks>different property for saving, because input is a string, but needs to be saved as an int</remarks>
        internal string PullTimeoutInternal
        {
            set
            {
                int timeout;

                if (String.IsNullOrEmpty(value))
                {
                    timeout = DefaultPullTimeout;
                }
                else
                {
                    if (!int.TryParse(value, out timeout) || timeout < 1)
                    {
                        throw new ClientException(Resources.InputPullTimeoutInvalid, null);
                    }
                }

                Settings.Default.PullTimeout = timeout;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Checks if the config is complete (if none of the settings is empty)
        /// </summary>
        public bool IsComplete()
        {
            // don't check PullTimeout because it's optional!
            return this.UserName != string.Empty
                && this.PassWord != string.Empty
                && this.BackupFolder != string.Empty;
        }

        /// <summary>
        /// Asks for the config settings
        /// </summary>
        public void Input()
        {
            Console.WriteLine(Resources.InputUser);
            this.UserName = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine(Resources.InputPassword);

            string pw = string.Empty;
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Backspace:
                        if (pw.Length > 0)
                        {
                            pw = pw.Substring(0, pw.Length - 1);
                            Console.Write("\b \b");
                        }
                        break;

                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        break;

                    default:
                        pw += key.KeyChar;
                        Console.Write("*");
                        break;
                }
            }
            while (key.Key != ConsoleKey.Enter);

            this.PassWord = pw;

            Console.WriteLine();
            Console.WriteLine(Resources.InputBackupFolder);
            this.BackupFolder = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine(String.Format(Resources.InputPullTimeout, DefaultPullTimeout));
            this.PullTimeoutInternal = Console.ReadLine();
        }
    }
}
