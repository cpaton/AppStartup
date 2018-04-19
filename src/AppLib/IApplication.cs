using System.Threading.Tasks;

namespace AppLib
{
    public interface IApplication
    {
        /// <summary>
        /// Called to start and initialise the application
        /// </summary>
        /// <remarks>
        /// This method will be called on a thread designated for the application by the host.
        /// </remarks>
        Task Start();

        Task Stop();
    }
}