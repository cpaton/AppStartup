using System;

namespace AppLib.CommandLine
{
    /// <summary>
    /// Binds a command line type to the application that it can be used with
    /// </summary>
    public interface ICommandLineBinding
    {
        Type CommandLineType { get; }
        IBootstrapper CreateBootstrapper(object commandLine);
    }

    /// <summary>
    /// Binds a command line type to the application that it can be used with
    /// </summary>
    public interface ICommandLineBinding<in TCommandLine> : ICommandLineBinding
    {
        IBootstrapper CreateBootstrapper(TCommandLine commandLine);
    }
}