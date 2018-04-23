namespace AppLib.CommandLine
{
    public interface ITypedCommandLineParser
    {
        void Parse<TCommandLine>();
    }
}