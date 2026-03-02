// Assets/Scripts/BossRaid/Core/Loop/Occurred/PlayerAttackOccurred.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Occurred
{
    public readonly struct PlayerAttackOccurred : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(PlayerAttackOccurred);

        public readonly string actorId;
        public readonly int appliedDamage;

        public PlayerAttackOccurred(string actorId, int appliedDamage, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.actorId = actorId;
            this.appliedDamage = appliedDamage;
        }
    }
}
