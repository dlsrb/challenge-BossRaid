using BossRaid.Core.Events;
using BossRaid.Gameplay.Weapons;

namespace BossRaid.Core.Events.Requested
{
    public sealed class WeaponEquipRequested : GameEventBase
    {
        public override string EventName => nameof(WeaponEquipRequested);

        public string ActorId { get; }
        public WeaponDefinitionSO Weapon { get; }

        public WeaponEquipRequested(string sourceId, string actorId, WeaponDefinitionSO weapon)
            : base(sourceId)
        {
            ActorId = actorId;
            Weapon = weapon;
        }
    }
}
