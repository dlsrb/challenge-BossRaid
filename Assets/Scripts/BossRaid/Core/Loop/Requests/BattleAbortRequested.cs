// Assets/Scripts/BossRaid/Core/Loop/Requests/BattleAbortRequested.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Requests
{
    public readonly struct BattleAbortRequested : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(BattleAbortRequested);

        public readonly string actorId;
        public readonly string reason;

        public BattleAbortRequested(string actorId, string reason, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.actorId = actorId;
            this.reason = reason;
        }
    }
}
