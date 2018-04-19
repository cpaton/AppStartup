using System;
using System.Threading.Tasks;
using AppLib;

namespace SampleApp.Red
{
    public class RedSampleApp : IApplication
    {
        public Task Start()
        {
            Console.WriteLine("Starting");
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            Console.WriteLine("Stopping");
            return Task.CompletedTask;
        }
    }
}
