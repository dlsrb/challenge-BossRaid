namespace BossRaid.Core.Events
{
    // "이 이벤트를 받는다"까지만 정의. 무엇을 할지는 (중재자) Step 3에서.
    public interface IGameEventHandler<in TEvent> where TEvent : IGameEvent
    {
        void Handle(TEvent gameEvent);
    }
}
