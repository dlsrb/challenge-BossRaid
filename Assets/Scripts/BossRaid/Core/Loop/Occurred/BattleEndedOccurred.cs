// Assets/Scripts/BossRaid/Core/Loop/Occurred/BattleEndedOccurred.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Occurred
{
    public readonly struct BattleEndedOccurred : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(BattleEndedOccurred);

        public readonly float endTime;

        public BattleEndedOccurred(float endTime, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.endTime = endTime;
        }
    }
}
