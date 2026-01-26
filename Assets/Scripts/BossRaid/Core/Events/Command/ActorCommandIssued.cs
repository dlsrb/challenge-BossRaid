using BossRaid.Core.Commands;
using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Command
{
    public sealed class ActorCommandIssued : GameEventBase
    {
        public override string EventName => "ActorCommandIssued";

        public string TargetActorId { get; }
        public IActorCommand Command { get; }

        public ActorCommandIssued(string sourceId, string targetActorId, IActorCommand command)
            : base(sourceId)
        {
            TargetActorId = targetActorId;
            Command = command;
        }

        public override string ToString()
            => $"[Command] {EventName} Target={TargetActorId}, Cmd={Command}, Source={SourceId}";
    }
}
