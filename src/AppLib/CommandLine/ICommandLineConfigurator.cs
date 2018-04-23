namespace AppLib.CommandLine
{
    public interface ICommandLineConfigurator<out TConfiguration>
    {
        TConfiguration ToConfiguration();
    }
}