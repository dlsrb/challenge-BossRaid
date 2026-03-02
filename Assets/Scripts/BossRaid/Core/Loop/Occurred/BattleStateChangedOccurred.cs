// Assets/Scripts/BossRaid/Core/Loop/Occurred/BattleStateChangedOccurred.cs
using System;
using BossRaid.Core.Events;
using BossRaid.Core.Loop;

namespace BossRaid.Core.Loop.Occurred
{
    public readonly struct BattleStateChangedOccurred : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(BattleStateChangedOccurred);

        public readonly CoreLoopState from;
        public readonly CoreLoopState to;

        public BattleStateChangedOccurred(CoreLoopState from, CoreLoopState to, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.from = from;
            this.to = to;
        }
    }
}
