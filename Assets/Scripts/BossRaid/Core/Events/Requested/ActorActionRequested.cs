using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Requested
{
    public sealed class ActorActionRequested : GameEventBase
    {
        public override string EventName => "ActorActionRequested";

        public string ActorId { get; }
        public string ActionId { get; }

        public ActorActionRequested(string sourceId, string actorId, string actionId)
            : base(sourceId)
        {
            ActorId = actorId;
            ActionId = actionId;
        }

        public override string ToString()
            => $"[Requested] {EventName} ActorId={ActorId}, ActionId={ActionId}, Source={SourceId}";
    }
}
