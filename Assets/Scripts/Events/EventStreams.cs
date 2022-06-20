using SimpleEventBus;

namespace Events
{
    public static class EventStreams
    {
        public static EventBus UserInterface { get; } = new EventBus();
    }
}