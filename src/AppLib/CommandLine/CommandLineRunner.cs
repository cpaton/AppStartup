using System;
using System.Linq;
using System.Threading.Tasks;
using AppLib.Host;
using AppLib.Initialisation;

namespace AppLib.CommandLine
{
    public sealed class ReturnCodes
    {
        public const int Success = 0;
        public const int CommandLineParsingFailed = -1;
        public const int BoostrapFailed = -2;
    }

    /// <summary>
    /// Runs an application with support for configuration from the command line
    /// </summary>
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
                    return ReturnCodes.CommandLineParsingFailed;
                case ParseResult.SuccessfulAndExit:
                    consoleHost.ReportInitialisation(initialisationInformation);
                    return ReturnCodes.Success;
                default:
                    var applicationBootstrapper = commandLineBinding.CreateBootstrapper(parseResult.CommandLine);
                    IApplication application = null;

                    try
                    {
                        application = applicationBootstrapper.Bootstrap();
                    }
                    catch (Exception ex)
                    {
                        initialisationInformation.AddMessage(MessageType.Error, $"Failed to bootstrap{Environment.NewLine}{ex}");
                        consoleHost.ReportInitialisation(initialisationInformation);
                        return ReturnCodes.BoostrapFailed;
                    }

                    var returnCode = await consoleHost.Run(application, initialisationInformation);

                    return (int)returnCode;
            }
        }
    }
}
