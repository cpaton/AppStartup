using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLib.Host;
using AppLib.Initialisation;
using CommandLine;

namespace AppLib
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

            object commandLine = null;

            var stringBuilder = new StringBuilder();
            using (var writer = new StringWriter(stringBuilder))
            using (var parser = new Parser(x =>
                                {
                                    x.IgnoreUnknownArguments = true;
                                    x.HelpWriter = writer;
                                }))
            {
                var parserResult = parser.ParseArguments(args, commandLineBinding.CommandLineType);

                if (parserResult.Tag != ParserResultType.Parsed)
                {
                    initialisationInformation.AddMessage(MessageType.Error, "Failed to parse command line arguments");
                    initialisationInformation.AddMessage(MessageType.Information, stringBuilder.ToString());
                    consoleHost.ReportInitialisationError(initialisationInformation);
                    return -1;
                }

                parserResult.WithParsed(parsedCommandLine =>
                {
                    commandLine = parsedCommandLine;
                    var formatCommandLine = parser.FormatCommandLine(commandLine);
                    initialisationInformation.AddMessage(MessageType.Information, $"Command line interpreted as {Environment.NewLine}{formatCommandLine}");
                });
            }

            var applicationBootstrapper = commandLineBinding.CreateBootstrapper(commandLine);
            var application = applicationBootstrapper.Bootstrap();

            var returnCode = await consoleHost.Run(application, initialisationInformation);

            return (int) returnCode;
        }
    }
}
