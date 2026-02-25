// Assets/Scripts/BossRaid/Battle/Core/Mediators/WeaponMediator.cs
using System.Collections.Generic;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Command;
using BossRaid.Core.Events.Occurred;
using BossRaid.Core.Events.Requested;

namespace BossRaid.Core.Mediators
{
    // 무기 규칙/판단은 이 Mediator 단일 지점에서만 수행한다.
    public sealed class WeaponMediator :
        IGameEventHandler<WeaponEquipRequested>,
        IGameEventHandler<WeaponUseRequested>
    {
        private readonly IGameEventBus _bus;
        private readonly string _sourceId;

        // ActorId -> Equipped Weapon
        private readonly Dictionary<string, WeaponEquipRequested.WeaponSpec> _equipped = new Dictionary<string, WeaponEquipRequested.WeaponSpec>();

        public WeaponMediator(IGameEventBus bus, string sourceId)
        {
            _bus = bus;
            _sourceId = sourceId;
        }

        public void Handle(WeaponEquipRequested e)
        {
            // 판단(=유효성/규칙 확인)은 여기서 한다.
            if (string.IsNullOrEmpty(e.ActorId)) return;
            if (string.IsNullOrEmpty(e.Weapon.WeaponId)) return;

            _equipped[e.ActorId] = e.Weapon;

            // "무기가 장착되었다"는 발생 사실을 알린다.
            _bus.Publish(new WeaponEquippedOccurred(_sourceId, e.ActorId, e.Weapon.WeaponId));
        }

        public void Handle(WeaponUseRequested e)
        {
            // 규칙: 장착된 무기가 없으면 사용 요청을 무시한다(판단은 Mediator에서만).
            if (!_equipped.TryGetValue(e.ActorId, out var weapon))
                return;

            // Executor는 판단하지 않는다. Mediator가 상태/규칙을 해석해 실행 Command로 변환한다.
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
