// Assets/Scripts/BossRaid/Core/Loop/Commands/ExecutePlayerAttackCommand.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop.Commands
{
    public readonly struct ExecutePlayerAttackCommand : IGameEvent
    {
        public DateTime UtcTime { get; }
        public string SourceId { get; }
        public string EventName => nameof(ExecutePlayerAttackCommand);

        public readonly string actorId;
        public readonly int damage; // Step7 임시 고정 수치(밸런스 아님)

        public ExecutePlayerAttackCommand(string actorId, int damage, string sourceId = "Step7")
        {
            UtcTime = DateTime.UtcNow;
            SourceId = sourceId;
            this.actorId = actorId;
            this.damage = damage;
        }
    }
}
