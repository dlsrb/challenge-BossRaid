using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Occurred
{
    public sealed class WeaponEquippedOccurred : GameEventBase
    {
        public override string EventName => nameof(WeaponEquippedOccurred);

        public string ActorId { get; }
        public string WeaponId { get; }

        public WeaponEquippedOccurred(string sourceId, string actorId, string weaponId)
            : base(sourceId)
        {
            ActorId = actorId;
            WeaponId = weaponId;
        }
    }
}
