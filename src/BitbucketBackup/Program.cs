﻿using System;
using Fclp;
using Ninject;

namespace BitbucketBackup
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var compositeLogger = new CompositeLogger())
            {
                var p = new FluentCommandLineParser();
                p.Setup<string>('e', "email")
                    .Callback(delegate(string emailAddress)
                    {
                        if (!String.IsNullOrEmpty(emailAddress))
                        {
                            compositeLogger.AddLogger(new EmailLogger(emailAddress));
                        }
                    });
                p.Parse(args);

                compositeLogger.AddLogger(new ConsoleLogger());

                var kernel = new StandardKernel();

                kernel.Bind<IBitbucketBackup>().To<BitbucketBackup>();
                kernel.Bind<IBitbucketRequest>().To<BitbucketRequest>();
                kernel.Bind<IResponseParser>().To<ResponseParser>();
                kernel.Bind<IRepositoryUpdater>().To<RepositoryUpdater>();
                kernel.Bind<IRepositoryFactory>().To<RepositoryFactory>();
                kernel.Bind<IConfig>().To<Config>().InSingletonScope();
                kernel.Bind<ILogger>().ToConstant(compositeLogger);

                kernel.Get<IBitbucketBackup>().Execute();
            }
        }
    }
}