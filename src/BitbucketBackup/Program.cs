
namespace BitbucketBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Config();
            var request = new BitbucketRequest();

            string resource = "users/" + config.UserName;
            string response = request.Execute(resource);
        }
    }
}
