using BossRaid.Core.Events;
using BossRaid.Gameplay.Weapons;

namespace BossRaid.Core.Events.Occurred
{
    public sealed class WeaponEquippedOccurred : GameEventBase
    {
        public override string EventName => nameof(WeaponEquippedOccurred);

        public string ActorId { get; }
        public WeaponDefinitionSO Weapon { get; }

        public WeaponEquippedOccurred(string sourceId, string actorId, WeaponDefinitionSO weapon)
            : base(sourceId)
        {
            ActorId = actorId;
            Weapon = weapon;
        }
    }
}
