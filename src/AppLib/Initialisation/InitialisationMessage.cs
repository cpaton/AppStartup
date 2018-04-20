namespace AppLib.Initialisation
{
    public class InitialisationMessage
    {
        public MessageType Type { get; }
        public string Message { get; }

        public InitialisationMessage(MessageType type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}