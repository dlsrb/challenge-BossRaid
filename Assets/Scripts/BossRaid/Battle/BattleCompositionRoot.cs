using UnityEngine;
using BossRaid.Core.Events;
using BossRaid.Core.Mediators;
using BossRaid.Core.Events.Requested;
using BossRaid.Core.Events.Command;
using BossRaid.Gameplay.Boss;

public sealed class BattleCompositionRoot : MonoBehaviour
{
    [SerializeField] private EventLayerContext eventLayerContext;
    [SerializeField] private BossActorExecutor boss;

    private BattleMediator _mediator;

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

        if (eventLayerContext.Bus == null)
        {
            Debug.LogError("[BattleCompositionRoot] eventLayerContext.Bus is NULL (Awake 순서 문제)");
            return;
        }

        var bus = eventLayerContext.Bus;

        _mediator = new BattleMediator(bus, "Mediator");

        bus.Register<ActorActionRequested>(_mediator);
        bus.Register<ActorCommandIssued>(boss);

        boss.Init(bus);
    }
}
