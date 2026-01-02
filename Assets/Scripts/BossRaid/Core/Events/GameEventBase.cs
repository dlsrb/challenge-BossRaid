using System;

namespace BossRaid.Core.Events
{
    public abstract class GameEventBase : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public abstract string EventName { get; }

        protected GameEventBase(string sourceId)
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
        }
    }
}
