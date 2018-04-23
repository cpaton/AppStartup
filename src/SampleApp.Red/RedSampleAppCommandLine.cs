using AppLib.CommandLine;
using CommandLine;

namespace SampleApp.Red
{
    [Verb("red-app", HelpText = "Runs the red sample application")]
    public class RedSampleAppCommandLine : IRedSampleAppConfiguration, ICommandLineConfigurator<IRedSampleAppConfiguration>
    {
        [Option('n', "name", Default = "Default Name", HelpText = "Name parameter")]
        public string Name { get; set; } = "Default Value";

        [Option('v', "verbose", HelpText = "Specify to turn on debug logging")]
        public bool Verbose { get; set; }

        public IRedSampleAppConfiguration ToConfiguration()
        {
            return this;
        }
    }
}