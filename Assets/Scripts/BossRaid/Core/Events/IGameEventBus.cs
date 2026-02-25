namespace BossRaid.Core.Events
{
    public interface IGameEventBus
    {
        void Register<TEvent>(IGameEventHandler<TEvent> handler) where TEvent : IGameEvent;
        void Unregister<TEvent>(IGameEventHandler<TEvent> handler) where TEvent : IGameEvent;
        void Publish<TEvent>(TEvent gameEvent) where TEvent : IGameEvent;
    }
}
