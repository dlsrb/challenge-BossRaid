// Assets/Scripts/BossRaid/Core/Loop/Occurred/BattleResultOccurred.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Occurred
{
    public enum BattleResultType { Victory = 0, Defeat = 1 }

    public readonly struct BattleResultOccurred : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(BattleResultOccurred);

        public readonly BattleResultType result;
        public readonly string reason;

        public BattleResultOccurred(BattleResultType result, string reason, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.result = result;
            this.reason = reason;
        }
    }
}
