using SimpleEventBus;
using SimpleEventBus.Events;

namespace Events
{
    public class EventResourceShortage : EventBase
    {
        public string Message { get; }

        public EventResourceShortage(string message)
        {
            Message = message;
        }
    }
}