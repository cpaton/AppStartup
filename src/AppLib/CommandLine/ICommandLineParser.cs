using AppLib.Initialisation;

namespace AppLib.CommandLine
{
    internal interface ICommandLineParser
    {
        (ParseResult ParseResult, object CommandLine) Parse(string[] args, InitialisationInformation initialisationInformation);
    }
}