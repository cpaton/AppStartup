using System;
using System.Threading;
using System.Threading.Tasks;
using AppLib.Initialisation;

namespace AppLib.Host
{
    /// <summary>
    /// Runs an application from a console application.
    /// Application is started and the program doesn't return to the caller until the enter key is pressed.
    /// When enter is pressed the application is told to shutdown and the program exits
    /// </summary>
    public class ConsoleHost
    {
        public async Task<StatusCode> Run(IApplication application, InitialisationInformation initialisationInformation)
        {
            var startTaskSource = new TaskCompletionSource<int>();
            var appThread = new Thread(async () =>
            {
                try
                {
                    await application.Start(initialisationInformation);
                    startTaskSource.SetResult(0);
                }
                catch (Exception ex)
                {
                    startTaskSource.SetException(ex);
                }
            });

            Console.WriteLine("Host starting application");
            appThread.Start();

            try
            {
                await startTaskSource.Task;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to start - {ex}");
                return StatusCode.FailedToStart;
            }

            Console.WriteLine("Press enter to stop");
            Console.ReadLine();

            try
            {
                await application.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to stop - {ex}");
                return StatusCode.FailedToStop;
            }

            return StatusCode.Success;
        }

        public void ReportInitialisation(IInitialisationInformation initialisationInformation)
        {
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
        }
    }
}
