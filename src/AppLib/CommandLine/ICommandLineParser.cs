using AppLib.Initialisation;

namespace AppLib.CommandLine
{
    /// <summary>
    /// Attempts to convert arguments passed in on the command line to an object representation
    /// </summary>
    internal interface ICommandLineParser
    {
        (ParseResult ParseResult, object CommandLine) Parse(string[] args, InitialisationInformation initialisationInformation);
    }
}