using AppLib;

namespace SampleApp.Red
{
    public class RedSampleAppCommandLineBinding : CommandLineBinding<RedSampleAppCommandLine, IRedSampleAppConfiguration,
        RedSampleAppBootstrapper>
    {
        public RedSampleAppCommandLineBinding() : base(config => new RedSampleAppBootstrapper(config))
        {
        }
    }
}