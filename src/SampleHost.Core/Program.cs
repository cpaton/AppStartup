using AppLib;
using SampleApp.Red;
using System;
using System.Threading.Tasks;

namespace SampleHost.Core
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            return await CommandLineRunner.Run<RedSampleAppCommandLineBinding>(args);
        }
    }
}
