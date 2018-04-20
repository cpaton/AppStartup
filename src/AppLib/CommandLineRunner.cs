using System;
using System.Threading.Tasks;
using AppLib.Host;

namespace AppLib
{
    public static class CommandLineRunner
    {
        public static async Task<int> Run<TCommandLineBinding>(string[] args) where TCommandLineBinding : ICommandLineBinding, new()
        {
            return await Run(args, new TCommandLineBinding());
        }

        public static async Task<int> Run<T>(string[] args, T commandLineBinding) where T : ICommandLineBinding
        {
            var consoleHost = new ConsoleHost();

            var commandLine = Activator.CreateInstance(commandLineBinding.CommandLineType);
            var applicationBootstrapper = commandLineBinding.CreateBootstrapper(commandLine);
            var application = applicationBootstrapper.Bootstrap();

            var returnCode = await consoleHost.Run(application);

            return (int) returnCode;
        }
    }
}
