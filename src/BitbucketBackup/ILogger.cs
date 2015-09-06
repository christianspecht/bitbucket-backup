namespace BitbucketBackup
{
    interface ILogger
    {
        void WriteLine();
        void WriteLine(string msg, params object[] args);
        void Flush();
    }
}
