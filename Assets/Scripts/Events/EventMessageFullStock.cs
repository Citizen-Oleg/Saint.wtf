using SimpleEventBus;
using SimpleEventBus.Events;

namespace Events
{
    public class EventMessageFullStock : EventBase
    {
        public string Message { get; }

        public EventMessageFullStock(string message)
        {
            Message = message;
        }
    }
}