using System;
using System.Threading;
using System.Threading.Tasks;
using AppLib;
using AppLib.Initialisation;
using log4net;

namespace SampleApp.Red
{
    public class RedSampleApp : IApplication
    {
        private readonly IRedSampleAppConfiguration _configuration;
        private readonly ILog _log;

        public RedSampleApp(IRedSampleAppConfiguration configuration, ILog log)
        {
            _configuration = configuration;
            _log = log;
        }

        public Task Start(InitialisationInformation initialisationInformation)
        {
            Console.WriteLine("Starting");
            foreach (var initialisationInformationMessage in initialisationInformation.Messages)
            {
                switch (initialisationInformationMessage.Type)
                {
                    case MessageType.Error:
                        _log.Error(initialisationInformationMessage.Message);
                        break;
                    default:
                        _log.Info(initialisationInformationMessage.Message);
                        break;
                }
            }
            ThreadPool.QueueUserWorkItem(_ =>
            {
                _log.Debug("Debug logging enabled");
                _log.Info(_configuration.Name);
            });
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            Console.WriteLine("Stopping");
            return Task.CompletedTask;
        }
    }
}
