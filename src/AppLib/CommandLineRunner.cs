using System.Threading.Tasks;
using AppLib.Host;

namespace AppLib
{
    public static class CommandLineRunner
    {
        public static async Task<int> Run<TApplication>(string[] args) where TApplication : IApplication, new()
        {
            var consoleHost = new ConsoleHost();
            var returnCode = await consoleHost.Run(new TApplication());

            return (int) returnCode;
        }
    }
}
