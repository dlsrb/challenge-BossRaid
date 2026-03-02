// Assets/Scripts/BossRaid/Core/Loop/Commands/EndBattleCommand.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Commands
{
    public readonly struct EndBattleCommand : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(EndBattleCommand);

        public readonly string actorId;
        public readonly string reason;

        public EndBattleCommand(string actorId, string reason, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.actorId = actorId;
            this.reason = reason;
        }
    }
}
