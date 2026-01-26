using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Occurred
{
    public sealed class ActorActionOccurred : GameEventBase
    {
        public override string EventName => "ActorActionOccurred";

        public string ActorId { get; }
        public string ActionId { get; }

        public ActorActionOccurred(string sourceId, string actorId, string actionId)
            : base(sourceId)
        {
            ActorId = actorId;
            ActionId = actionId;
        }

        public override string ToString()
            => $"[Occurred] {EventName} ActorId={ActorId}, ActionId={ActionId}, Source={SourceId}";
    }
}
