using System;
using System.IO;
using System.Text;
using AppLib.Initialisation;
using CommandLine;

namespace AppLib
{
    internal class CommandLineParser<TCommandLine> : ICommandLineParser
    {
        public (ParseResult ParseResult, object CommandLine) Parse(string[] args, InitialisationInformation initialisationInformation)
        {
            var stringBuilder = new StringBuilder();
            using (var writer = new StringWriter(stringBuilder))
            using (var parser = new Parser(x =>
            {
                x.IgnoreUnknownArguments = true;
                x.HelpWriter = writer;
            }))
            {
                var parserResult = parser.ParseArguments<TCommandLine>(args);

                if (parserResult.Tag != ParserResultType.Parsed)
                {
                    if (parserResult.IsRequestToShowHelp() || parserResult.IsRequestToShowVerbHelp() ||
                        parserResult.IsRequestToShowVersion())
                    {
                        initialisationInformation.AddMessage(MessageType.Information, stringBuilder.ToString());
                        return (ParseResult.SuccessfulAndExit, null);
                    }

                    initialisationInformation.AddMessage(MessageType.Error, "Failed to parse command line arguments");
                    initialisationInformation.AddMessage(MessageType.Information, stringBuilder.ToString());
                    return (ParseResult.Failed, null);
                }

                object commandLine = null;
                parserResult.WithParsed(parsedCommandLine =>
                {
                    commandLine = parsedCommandLine;
                    var formatCommandLine = parser.FormatCommandLine(commandLine);
                    initialisationInformation.AddMessage(MessageType.Information,
                                                          $"Command line interpreted as {Environment.NewLine}{formatCommandLine}");
                });
                return (ParseResult.Successful, commandLine);
            }
        }
    }
}