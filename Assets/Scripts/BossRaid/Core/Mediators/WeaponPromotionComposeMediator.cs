using System.Collections.Generic;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Command;
using BossRaid.Core.Events.Occurred;
using BossRaid.Core.Events.Requested;
using BossRaid.Core.Mediators.PromotionEffects;

namespace BossRaid.Core.Mediators
{
    public sealed class WeaponPromotionComposeMediator :
        IGameEventHandler<WeaponEquipRequested>,
        IGameEventHandler<WeaponUseRequested>
    {
        private readonly IGameEventBus _bus;
        private readonly string _sourceId;
        private readonly PromotionMediator _promotionMediator;
        private readonly PromotionEffectRuleRegistry _rules;
        private readonly WeaponFamilyResolver _weaponFamilyResolver;
        private readonly Dictionary<string, WeaponEquipRequested.WeaponSpec> _equippedByActor =
            new Dictionary<string, WeaponEquipRequested.WeaponSpec>();

        public WeaponPromotionComposeMediator(
            IGameEventBus bus,
            string sourceId,
            PromotionMediator promotionMediator,
            PromotionEffectRuleRegistry rules,
            WeaponFamilyResolver weaponFamilyResolver)
        {
            _bus = bus;
            _sourceId = sourceId;
            _promotionMediator = promotionMediator;
            _rules = rules;
            _weaponFamilyResolver = weaponFamilyResolver;
        }

        public void Handle(WeaponEquipRequested gameEvent)
        {
            if (gameEvent == null) return;
            if (string.IsNullOrEmpty(gameEvent.ActorId)) return;
            if (string.IsNullOrEmpty(gameEvent.Weapon.WeaponId)) return;

            _equippedByActor[gameEvent.ActorId] = gameEvent.Weapon;
            _bus.Publish(new WeaponEquippedOccurred(_sourceId, gameEvent.ActorId, gameEvent.Weapon.WeaponId));
        }

        public void Handle(WeaponUseRequested gameEvent)
        {
            if (gameEvent == null) return;
            if (string.IsNullOrEmpty(gameEvent.ActorId)) return;
            if (!_equippedByActor.TryGetValue(gameEvent.ActorId, out var weapon)) return;

            string weaponFamilyId = null;
            _weaponFamilyResolver?.TryGetFamilyId(weapon.WeaponId, out weaponFamilyId);

            var ctx = new PromotionComposeContext
            {
                weaponId = weapon.WeaponId,
                weaponFamilyId = weaponFamilyId,
                promotionId = null,
                effectKeys = null,
                modifiers = null,
                damage = weapon.BaseDamage,
                cooldownSeconds = weapon.CooldownSeconds
            };

            if (_promotionMediator.TryGetSelected(out var promo) && promo != null)
            {
                if (!string.IsNullOrEmpty(ctx.weaponFamilyId) && promo.weaponFamilyId == ctx.weaponFamilyId)
                {
                    ctx.promotionId = promo.promotionId;
                    ctx.effectKeys = promo.effectKeys ?? new string[0];
                    ctx.modifiers = promo.modifiers;

                    for (int i = 0; i < ctx.effectKeys.Length; i++)
                    {
                        if (_rules.TryGet(ctx.effectKeys[i], out var rule))
                        {
                            rule.Apply(ref ctx);
                        }
                    }

                    _bus.Publish(new PromotionContextOccurred(
                        _sourceId,
                        ctx.weaponId,
                        ctx.weaponFamilyId,
                        ctx.promotionId,
                        ctx.effectKeys
                    ));
                }
            }

            var cmd = new WeaponAttackCommand(
                weaponId: weapon.WeaponId,
                damage: ctx.damage,
                animTrigger: weapon.AttackAnimTrigger,
                specialKey: weapon.SpecialKey
            );
            _bus.Publish(new PromotionContextOccurred(
    _sourceId,
    weaponId: weapon.WeaponId,
    weaponFamilyId: ctx.weaponFamilyId,
    promotionId: ctx.promotionId,
    effectKeys: ctx.effectKeys ?? new string[0]
));

_bus.Publish(new WeaponAttackCommandIssued(_sourceId, gameEvent.ActorId, cmd));
        }
    }
}
