using System;
using System.Threading;
using System.Threading.Tasks;
using AppLib;

namespace SampleApp.Red
{
    public class RedSampleApp : IApplication
    {
        private readonly IRedSampleAppConfiguration _configuration;

        public RedSampleApp(IRedSampleAppConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task Start()
        {
            Console.WriteLine("Starting");
            ThreadPool.QueueUserWorkItem(_ => Console.WriteLine(_configuration.Name));
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            Console.WriteLine("Stopping");
            return Task.CompletedTask;
        }
    }
}
