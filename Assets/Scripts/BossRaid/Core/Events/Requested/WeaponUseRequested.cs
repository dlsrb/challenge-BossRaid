using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Requested
{
    public sealed class WeaponUseRequested : GameEventBase
    {
        public override string EventName => nameof(WeaponUseRequested);

        public string ActorId { get; }

        public WeaponUseRequested(string sourceId, string actorId)
            : base(sourceId)
        {
            ActorId = actorId;
        }
    }
}
