using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Requested
{
    public sealed class WeaponEquipRequested : GameEventBase
    {
        public override string EventName => nameof(WeaponEquipRequested);

        public string ActorId { get; }
        public WeaponSpec Weapon { get; }

        public WeaponEquipRequested(string sourceId, string actorId, WeaponSpec weapon)
            : base(sourceId)
        {
            ActorId = actorId;
            Weapon = weapon;
        }

        // Core-only payload (Gameplay/Unity 오브젝트 참조 금지).
        public readonly struct WeaponSpec
        {
            public readonly string WeaponId;
            public readonly float BaseDamage;
            public readonly float CooldownSeconds;
            public readonly string AttackAnimTrigger;
            public readonly string SpecialKey;

            public WeaponSpec(string weaponId, float baseDamage, float cooldownSeconds, string attackAnimTrigger, string specialKey)
            {
                WeaponId = weaponId;
                BaseDamage = baseDamage;
                CooldownSeconds = cooldownSeconds;
                AttackAnimTrigger = attackAnimTrigger;
                SpecialKey = specialKey;
            }
        }
    }
}
