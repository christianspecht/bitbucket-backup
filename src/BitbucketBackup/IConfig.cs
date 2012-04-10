
namespace BitbucketBackup
{
    /// <summary>
    /// helper class for getting values out of the config file
    /// </summary>
    internal interface IConfig
    {
        /// <summary>
        /// Bitbucket password
        /// </summary>
        string PassWord { get; }

        /// <summary>
        /// Bitbucket username
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Folder on local machine where backups are saved
        /// </summary>
        string BackupFolder { get; }

        /// <summary>
        /// Timeout for pulling (only for Mercurial at the moment)
        /// </summary>
        int PullTimeout { get; }

        /// <summary>
        /// Checks if the config is complete (if none of the settings is empty)
        /// </summary>
        bool IsComplete();

        /// <summary>
        /// Asks for the config settings
        /// </summary>
        void Input();
    }
}
