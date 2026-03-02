// Assets/Scripts/BossRaid/Core/Loop/Commands/EnterBattleCommand.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Commands
{
    public readonly struct EnterBattleCommand : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(EnterBattleCommand);

        public readonly string actorId;

        public EnterBattleCommand(string actorId, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.actorId = actorId;
        }
    }
}
