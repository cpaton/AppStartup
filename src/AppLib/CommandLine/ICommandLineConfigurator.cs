namespace AppLib.CommandLine
{
    /// <summary>
    /// Converts a command line into the type used to configure an application.
    /// </summary>
    /// <remarks>
    /// Most common scenario is for the command line type to be mutable to support parsing from the
    /// passed in arguments, and for the configuration type to be an immutable interface over the top
    /// exposing read-only access to the information
    /// </remarks>
    /// <typeparam name="TConfiguration"></typeparam>
    public interface ICommandLineConfigurator<out TConfiguration>
    {
        TConfiguration ToConfiguration();
    }
}