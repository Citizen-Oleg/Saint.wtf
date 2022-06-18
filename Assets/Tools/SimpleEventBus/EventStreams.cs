using SimpleEventBus.Interfaces;

namespace SimpleEventBus
{
    public static class EventStreams
    {
        public static IEventBus UserInterface { get; } = new EventBus();
    }
}