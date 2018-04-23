using AppLib.Autofac;
using Autofac;

namespace SampleApp.Red
{
    public class RedSampleAppBootstrapper : AutofacBootstrapper<RedSampleApp>
    {
        public RedSampleAppBootstrapper(IRedSampleAppConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IRedSampleAppConfiguration Configuration { get; }

        protected override void ConfigureLogging(ContainerBuilder containerBuilder)
        {
            RegisterLog4Net(containerBuilder, debugLogging: Configuration.Verbose);
        }

        protected override void BuildContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(Configuration).As<IRedSampleAppConfiguration>();
        }
    }
}