using UnityEngine;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Occurred;
using BossRaid.Core.Mediators;
using BossRaid.Core.Events.Requested;
using BossRaid.Core.Events.Command;
using BossRaid.Gameplay.Boss;
using BossRaid.Gameplay.Player;

// ✅ Step 5 추가: Weapon 관련 using (파일/네임스페이스가 이 경로가 맞다는 전제)
using BossRaid.Core.Mediators;            // WeaponMediator가 여기에 있다면 그대로
using BossRaid.Core.Events.Requested;     // WeaponEquipRequested, WeaponUseRequested
using BossRaid.Core.Events.Command;       // WeaponAttackCommandIssued

public sealed class BattleCompositionRoot : MonoBehaviour
{
    [SerializeField] private EventLayerContext eventLayerContext;
    [SerializeField] private BossActorExecutor boss;

    // ✅ Step 5 추가: PlayerRoot에 붙일 실행자(Inspector 연결)
    [SerializeField] private PlayerWeaponExecutor playerWeaponExecutor;

    private BattleMediator _mediator;

    // ✅ Step 5 추가: 무기 판단자(중재자)
    private WeaponMediator _weaponMediator;

    private void Start()
    {
        if (eventLayerContext == null)
        {
            Debug.LogError("[BattleCompositionRoot] eventLayerContext is NULL (Inspector 연결 확인)");
            return;
        }

        if (boss == null)
        {
            Debug.LogError("[BattleCompositionRoot] boss is NULL (Inspector 연결 확인)");
            return;
        }

        // ✅ Step 5 추가: 실행자 null 체크(기존 로직 변경 아님, 안전장치 추가)
        if (playerWeaponExecutor == null)
        {
            Debug.LogError("[BattleCompositionRoot] playerWeaponExecutor is NULL (PlayerRoot에 붙이고 Inspector 연결 확인)");
            return;
        }

        if (eventLayerContext.Bus == null)
        {
            Debug.LogError("[BattleCompositionRoot] eventLayerContext.Bus is NULL (Awake 순서 문제)");
            return;
        }

        var bus = eventLayerContext.Bus;

        _mediator = new BattleMediator(bus, "Mediator");

        bus.Register<ActorActionRequested>(_mediator);
        bus.Register<ActorCommandIssued>(boss);

        // ✅ Step 5 추가: 무기 중재자 생성 + 등록
        _weaponMediator = new WeaponMediator(bus, "WeaponMediator");
        bus.Register<WeaponEquipRequested>(_weaponMediator);
        bus.Register<WeaponUseRequested>(_weaponMediator);

        // ✅ Step 5 추가: 무기 공격 명령은 Player 실행자가 받는다
        bus.Register<WeaponAttackCommandIssued>(playerWeaponExecutor);

        boss.Init(bus);

        // ✅ Step 5 추가: 실행자 초기화
        playerWeaponExecutor.Init(bus);
    }
}
