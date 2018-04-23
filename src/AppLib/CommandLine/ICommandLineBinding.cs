using System;

namespace AppLib.CommandLine
{
    public interface ICommandLineBinding
    {
        Type CommandLineType { get; }
        IBootstrapper CreateBootstrapper(object commandLine);
    }

    public interface ICommandLineBinding<in TCommandLine> : ICommandLineBinding
    {
        IBootstrapper CreateBootstrapper(TCommandLine commandLine);
    }
}