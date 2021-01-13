using SQLI.SRS2.Services.Interfaces;

namespace SQLI.SRS2.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
