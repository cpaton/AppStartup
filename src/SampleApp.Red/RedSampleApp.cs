using System;
using System.Threading;
using System.Threading.Tasks;
using AppLib;
using AppLib.Initialisation;

namespace SampleApp.Red
{
    public class RedSampleApp : IApplication
    {
        private readonly IRedSampleAppConfiguration _configuration;

        public RedSampleApp(IRedSampleAppConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task Start(InitialisationInformation initialisationInformation)
        {
            Console.WriteLine("Starting");
            foreach (var initialisationInformationMessage in initialisationInformation.Messages)
            {
                switch (initialisationInformationMessage.Type)
                {
                    case MessageType.Error:
                        var foregroundColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(initialisationInformationMessage.Message);
                        Console.ForegroundColor = foregroundColor;
                        break;
                    default:
                        Console.WriteLine(initialisationInformationMessage.Message);
                        break;

                }
            }
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
