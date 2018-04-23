using System.Threading.Tasks;
using AppLib.Initialisation;

namespace AppLib
{
    /// <summary>
    /// Represents a logical unit that provides a cohesive set of functionality
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// Called to start and initialise the application
        /// </summary>
        /// <param name="initialisationInformation"></param>
        /// <remarks>
        /// This method will be called on a thread designated for the application by the host.
        /// </remarks>
        Task Start(InitialisationInformation initialisationInformation);

        Task Stop();
    }
}