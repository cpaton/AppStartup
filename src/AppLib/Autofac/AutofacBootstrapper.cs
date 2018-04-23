using System;
using System.IO;
using System.Reflection;
using Autofac;
using log4net;
using log4net.Config;

namespace AppLib.Autofac
{
    public abstract class AutofacBootstrapper<TApplication> : IBootstrapper where TApplication : IApplication
    {
        public IApplication Bootstrap()
        {
            var containerBuilder = new ContainerBuilder();

            ConfigureLogging(containerBuilder);
            BuildContainer(containerBuilder);

            containerBuilder.RegisterType<TApplication>().As<IApplication>().AsSelf().SingleInstance();

            IContainer container = null;
            containerBuilder.Register(c => container).AsSelf();
            containerBuilder.RegisterBuildCallback(c => container = c);


            container = containerBuilder.Build();
            var application = container.Resolve<TApplication>();
            return application;
        }

        protected abstract void ConfigureLogging(ContainerBuilder containerBuilder);
        protected abstract void BuildContainer(ContainerBuilder containerBuilder);

        protected void RegisterLog4Net(ContainerBuilder containerBuilder, string log4NetConfigFilePath = "log4net.config")
        {
            var repositoryAssembly = typeof(TApplication).Assembly;
            containerBuilder.RegisterModule<LoggingModule>();

            if (!Path.IsPathRooted(log4NetConfigFilePath))
            {
                var entryAssemblyPath = Assembly.GetEntryAssembly().Location;
                var assemblyDirectory = Path.GetDirectoryName(entryAssemblyPath);
                log4NetConfigFilePath = Path.Combine(assemblyDirectory, log4NetConfigFilePath);
            }

            if (!File.Exists(log4NetConfigFilePath))
            {
                throw new ArgumentException($"log4Net config file '{log4NetConfigFilePath}' does not exist");
            }

            var log4NetConfigFileInfo = new FileInfo(log4NetConfigFilePath);
            var loggerRepository = LogManager.GetRepository(repositoryAssembly);
            XmlConfigurator.ConfigureAndWatch(loggerRepository, log4NetConfigFileInfo);
        }
    }
}
