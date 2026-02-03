using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Command
{
    public sealed class WeaponAttackCommandIssued : GameEventBase
    {
        public override string EventName => nameof(WeaponAttackCommandIssued);

        public string ActorId { get; }
        public WeaponAttackCommand Command { get; }

        public WeaponAttackCommandIssued(string sourceId, string actorId, WeaponAttackCommand command)
            : base(sourceId)
        {
            ActorId = actorId;
            Command = command;
        }
    }

    public readonly struct WeaponAttackCommand
    {
        public readonly string WeaponId;
        public readonly float Damage;
        public readonly string AnimTrigger;
        public readonly string SpecialKey;

        public WeaponAttackCommand(string weaponId, float damage, string animTrigger, string specialKey)
        {
            WeaponId = weaponId;
            Damage = damage;
            AnimTrigger = animTrigger;
            SpecialKey = specialKey;
        }
    }
}
