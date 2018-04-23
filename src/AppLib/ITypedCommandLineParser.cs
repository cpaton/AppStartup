namespace AppLib
{
    public interface ITypedCommandLineParser
    {
        void Parse<TCommandLine>();
    }
}