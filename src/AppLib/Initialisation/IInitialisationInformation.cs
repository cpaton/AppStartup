using System.Collections.Generic;

namespace AppLib.Initialisation
{
    public interface IInitialisationInformation
    {
        IList<InitialisationMessage> Messages { get; }
    }
}