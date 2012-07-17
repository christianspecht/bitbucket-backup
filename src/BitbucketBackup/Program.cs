using Ninject;

namespace BitbucketBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();

            kernel.Bind<IBitbucketBackup>().To<BitbucketBackup>();
            kernel.Bind<IBitbucketRequest>().To<BitbucketRequest>();
            kernel.Bind<IResponseParser>().To<ResponseParser>();
            kernel.Bind<IRepositoryUpdater>().To<RepositoryUpdater>();
            kernel.Bind<IRepositoryFactory>().To<RepositoryFactory>();
            kernel.Bind<IConfig>().To<Config>().InSingletonScope();

            kernel.Get<IBitbucketBackup>().Execute();
        }
    }
}