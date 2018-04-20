using AppLib;

namespace SampleApp.Red
{
    public class RedSampleAppBootstrapper : IBootstrapper
    {
        public RedSampleAppBootstrapper(IRedSampleAppConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IRedSampleAppConfiguration Configuration { get; }

        public IApplication Bootstrap()
        {
            return new RedSampleApp(Configuration);
        }
    }
}