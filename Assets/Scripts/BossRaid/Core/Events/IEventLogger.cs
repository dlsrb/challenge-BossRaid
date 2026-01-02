namespace BossRaid.Core.Events
{
    public interface IEventLogger
    {
        void Log(IGameEvent gameEvent);
    }
}
