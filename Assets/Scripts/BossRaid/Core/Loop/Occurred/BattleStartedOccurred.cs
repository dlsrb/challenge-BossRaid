// Assets/Scripts/BossRaid/Core/Loop/Occurred/BattleStartedOccurred.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Occurred
{
    public readonly struct BattleStartedOccurred : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(BattleStartedOccurred);

        public readonly float startTime;

        public BattleStartedOccurred(float startTime, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.startTime = startTime;
        }
    }
}
