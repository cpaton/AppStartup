using AppLib.Initialisation;

namespace AppLib
{
    internal interface ICommandLineParser
    {
        (ParseResult ParseResult, object CommandLine) Parse(string[] args, InitialisationInformation initialisationInformation);
    }
}