using UnityEngine;
using BossRaid.Core.Events;
using BossRaid.Core.Mediators;
using BossRaid.Core.Events.Requested;
using BossRaid.Core.Events.Command;
using BossRaid.Gameplay.Boss;
using BossRaid.Gameplay.Player;

public sealed class BattleCompositionRoot : MonoBehaviour
{
    [SerializeField] private EventLayerContext eventLayerContext;
    [SerializeField] private BossActorExecutor boss;

    // ✅ Step 5 추가: PlayerRoot에 붙일 실행자(Inspector 연결)
    [SerializeField] private PlayerWeaponExecutor playerWeaponExecutor;

    private BattleMediator _mediator;

    // ✅ Step 5 추가: 무기 판단자(중재자)
    private WeaponMediator _weaponMediator;

    private GameEventBus _bus;
    private bool _wired;

    private void Awake()
    {
        if (eventLayerContext == null)
        {
            Debug.LogError("[BattleCompositionRoot] eventLayerContext is NULL (check Inspector wiring)");
            return;
        }

        if (boss == null)
        {
            Debug.LogError("[BattleCompositionRoot] boss is NULL (check Inspector wiring)");
            return;
        }

        // ✅ Step 5 추가: 실행자 null 체크(기존 로직 변경 아님, 안전장치 추가)
        if (playerWeaponExecutor == null)
        {
            Debug.LogError("[BattleCompositionRoot] playerWeaponExecutor is NULL (attach and wire in the Inspector)");
            return;
        }

        eventLayerContext.EnsureInitialized();
        if (eventLayerContext.Bus == null)
        {
            Debug.LogError("[BattleCompositionRoot] eventLayerContext.Bus is NULL (initialization order)");
            return;
        }

        _bus = eventLayerContext.Bus;

        _mediator = new BattleMediator(_bus, "Mediator");

        _bus.Register<ActorActionRequested>(_mediator);
        _bus.Register<ActorCommandIssued>(boss);

        // ✅ Step 5 추가: 무기 중재자 생성 + 등록
        _weaponMediator = new WeaponMediator(_bus, "WeaponMediator");
        _bus.Register<WeaponEquipRequested>(_weaponMediator);
        _bus.Register<WeaponUseRequested>(_weaponMediator);

        // ✅ Step 5 추가: 무기 공격 명령은 Player 실행자가 받는다
        _bus.Register<WeaponAttackCommandIssued>(playerWeaponExecutor);

        boss.Init(_bus);

        // ✅ Step 5 추가: 실행자 초기화
        playerWeaponExecutor.Init(_bus);

        _wired = true;
    }

    private void OnDestroy()
    {
        if (!_wired || _bus == null) return;

        _bus.Unregister<ActorActionRequested>(_mediator);
        _bus.Unregister<ActorCommandIssued>(boss);

        _bus.Unregister<WeaponEquipRequested>(_weaponMediator);
        _bus.Unregister<WeaponUseRequested>(_weaponMediator);
        _bus.Unregister<WeaponAttackCommandIssued>(playerWeaponExecutor);
    }
}
