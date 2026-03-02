// Assets/Scripts/BossRaid/Core/Loop/Requests/BattleRetryRequested.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Requests
{
    public readonly struct BattleRetryRequested : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(BattleRetryRequested);

        public readonly string actorId;

        public BattleRetryRequested(string actorId, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.actorId = actorId;
        }
    }
}
