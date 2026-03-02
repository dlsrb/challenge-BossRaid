// Assets/Scripts/BossRaid/Core/Loop/Commands/StartBattleCommand.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Commands
{
    public readonly struct StartBattleCommand : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(StartBattleCommand);

        public readonly string actorId;

        public StartBattleCommand(string actorId, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.actorId = actorId;
        }
    }
}
