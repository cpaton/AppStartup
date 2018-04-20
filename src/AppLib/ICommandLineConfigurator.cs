namespace AppLib
{
    public interface ICommandLineConfigurator<out TConfiguration>
    {
        TConfiguration ToConfiguration();
    }
}