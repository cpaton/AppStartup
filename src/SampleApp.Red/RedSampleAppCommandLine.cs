using AppLib;

namespace SampleApp.Red
{
    public class RedSampleAppCommandLine : IRedSampleAppConfiguration, ICommandLineConfigurator<IRedSampleAppConfiguration>
    {
        public string Name { get; set; } = "Default Value";

        public IRedSampleAppConfiguration ToConfiguration()
        {
            return this;
        }
    }
}