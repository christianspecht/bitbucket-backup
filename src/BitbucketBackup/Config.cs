using System;
using System.IO;
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
        /// Checks if the config is complete (if none of the settings is empty)
        /// </summary>
        public bool IsComplete()
        {
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

            Console.WriteLine(Resources.InputBackupFolder);
            this.BackupFolder = Console.ReadLine();
        }
    }
}
