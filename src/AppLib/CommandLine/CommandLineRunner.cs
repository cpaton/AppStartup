using System;
using System.Linq;
using System.Threading.Tasks;
using AppLib.Host;
using AppLib.Initialisation;

namespace AppLib.CommandLine
{
    public static class CommandLineRunner
    {
        public static async Task<int> Run<TCommandLineBinding>(string[] args) where TCommandLineBinding : ICommandLineBinding, new()
        {
            return await Run(args, new TCommandLineBinding());
        }

        public static async Task<int> Run<T>(string[] args, T commandLineBinding) where T : ICommandLineBinding
        {
            var initialisationInformation = new InitialisationInformation();
            initialisationInformation.AddMessage(MessageType.Information, $"Received command line {string.Join(" ", args.Select(x => $"[{x}]"))}");
            var consoleHost = new ConsoleHost();

            var parserType = typeof(CommandLineParser<>).MakeGenericType(commandLineBinding.CommandLineType);
            var commandLineParser = (ICommandLineParser)Activator.CreateInstance(parserType);

            var parseResult = commandLineParser.Parse(args, initialisationInformation);

            switch (parseResult.ParseResult)
            {
                case ParseResult.Failed:
                    consoleHost.ReportInitialisation(initialisationInformation);
                    return -1;
                case ParseResult.SuccessfulAndExit:
                    consoleHost.ReportInitialisation(initialisationInformation);
                    return 0;
                default:
                    var applicationBootstrapper = commandLineBinding.CreateBootstrapper(parseResult.CommandLine);
                    var application = applicationBootstrapper.Bootstrap();

                    var returnCode = await consoleHost.Run(application, initialisationInformation);

                    return (int) returnCode;
            }
        }
    }
}
