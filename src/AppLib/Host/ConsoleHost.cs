using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppLib.Host
{
    public class ConsoleHost
    {
        public async Task<StatusCode> Run(IApplication application)
        {
            var startTaskSource = new TaskCompletionSource<int>();
            var appThread = new Thread(async () =>
            {
                try
                {
                    await application.Start();
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
    }
}
