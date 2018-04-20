using System.Collections.Generic;

namespace AppLib.Initialisation
{
    public class InitialisationInformation : IInitialisationInformation
    {
        private readonly List<InitialisationMessage> _messages = new List<InitialisationMessage>();

        public void AddMessage(MessageType type, string message)
        {
            _messages.Add(new InitialisationMessage(type, message));
        }

        public IList<InitialisationMessage> Messages => _messages;
    }
}