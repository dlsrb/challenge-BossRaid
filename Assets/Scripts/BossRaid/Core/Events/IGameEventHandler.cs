namespace BossRaid.Core.Events
{
    // "이 이벤트를 처리한다"는 선언용 인터페이스(구독자).
    public interface IGameEventHandler<in TEvent> where TEvent : IGameEvent
    {
        void Handle(TEvent gameEvent);
    }
}
