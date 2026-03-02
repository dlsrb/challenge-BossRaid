// Assets/Scripts/BossRaid/Core/Loop/Requests/BattleEnterRequested.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Requests
{
    public readonly struct BattleEnterRequested : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(BattleEnterRequested);

        public readonly string actorId;

        public BattleEnterRequested(string actorId, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.actorId = actorId;
        }
    }
}
