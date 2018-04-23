using System;

namespace AppLib.CommandLine
{
    public class CommandLineBinding<TCommandLine, TConfiguration, TBootstrapper> : ICommandLineBinding<TCommandLine> where TBootstrapper : IBootstrapper where TCommandLine : ICommandLineConfigurator<TConfiguration>, new()
    {
        private readonly Func<TConfiguration, TBootstrapper> _bootstrapperFactory;

        public CommandLineBinding(Func<TConfiguration, TBootstrapper> bootstrapperFactory)
        {
            _bootstrapperFactory = bootstrapperFactory;
        }

        public Type CommandLineType => typeof(TCommandLine);

        public IBootstrapper CreateBootstrapper(object commandLine)
        {
            if (commandLine is TCommandLine typedCommandLine)
            {
                return CreateBootstrapper(typedCommandLine);
            }
            throw new ArgumentException($"Given command line type {commandLine?.GetType().Name} is not the expected type {typeof(TCommandLine).Name}", nameof(commandLine));
        }

        public IBootstrapper CreateBootstrapper(TCommandLine commandLine)
        {
            var configuration = commandLine.ToConfiguration();
            return _bootstrapperFactory(configuration);
        }
    }
}