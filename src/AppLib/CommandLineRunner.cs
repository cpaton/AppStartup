using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AppLib.Host;
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
                    Console.WriteLine(stringBuilder.ToString());
                    return -1;
                }

                parserResult.WithParsed(parsedCommandLine =>
                {
                    commandLine = parsedCommandLine;
                });
            }

            var applicationBootstrapper = commandLineBinding.CreateBootstrapper(commandLine);
            var application = applicationBootstrapper.Bootstrap();

            var returnCode = await consoleHost.Run(application);

            return (int) returnCode;
        }
    }
}
