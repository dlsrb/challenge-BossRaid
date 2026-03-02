// Assets/Scripts/BossRaid/Core/Loop/Requests/PlayerAttackRequested.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Requests
{
    public readonly struct PlayerAttackRequested : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(PlayerAttackRequested);

        public readonly string actorId;

        public PlayerAttackRequested(string actorId, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.actorId = actorId;
        }
    }
}
