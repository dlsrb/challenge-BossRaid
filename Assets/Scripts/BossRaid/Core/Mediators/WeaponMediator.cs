// Assets/Scripts/BossRaid/Battle/Core/Mediators/WeaponMediator.cs
using System.Collections.Generic;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Command;
using BossRaid.Core.Events.Occurred;
using BossRaid.Core.Events.Requested;
using BossRaid.Gameplay.Weapons;

namespace BossRaid.Core.Mediators
{
    // 판단자는 여기 하나(Weapon 관련 규칙/해석)
    public sealed class WeaponMediator :
        IGameEventHandler<WeaponEquipRequested>,
        IGameEventHandler<WeaponUseRequested>
    {
        private readonly GameEventBus _bus;
        private readonly string _sourceId;

        // ActorId -> Equipped Weapon
        private readonly Dictionary<string, WeaponDefinitionSO> _equipped = new();

        public WeaponMediator(GameEventBus bus, string sourceId)
        {
            _bus = bus;
            _sourceId = sourceId;
        }

        public void Handle(WeaponEquipRequested e)
        {
            // 판단(=장착 확정)은 여기서만
            if (e.Weapon == null) return;

            _equipped[e.ActorId] = e.Weapon;

            // "장착되었다" 사실 보고
            _bus.Publish(new WeaponEquippedOccurred(_sourceId, e.ActorId, e.Weapon));
        }

        public void Handle(WeaponUseRequested e)
        {
            // 규칙/판단: 현재 장착 무기가 있어야 명령을 만든다(판단자는 Mediator)
            if (!_equipped.TryGetValue(e.ActorId, out var weapon) || weapon == null)
                return;

            // Weapon은 판단하지 않는다. Mediator가 Weapon 정의를 "읽어서" Command를 만든다.
            var cmd = new WeaponAttackCommand(
                weaponId: weapon.WeaponId,
                damage: weapon.BaseDamage,
                animTrigger: weapon.AttackAnimTrigger,
                specialKey: weapon.SpecialKey
            );

            _bus.Publish(new WeaponAttackCommandIssued(_sourceId, e.ActorId, cmd));
        }
    }
}
