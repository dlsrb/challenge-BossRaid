// Assets/Scripts/BossRaid/Battle/Loop/Step7Bootstrapper.cs
using UnityEngine;
using BossRaid.Core.Events;
using BossRaid.Core.Loop;
using BossRaid.Core.Mediators;

namespace BossRaid.Battle.Loop
{
    public sealed class Step7Bootstrapper : MonoBehaviour
    {
        [Header("References (drag in Inspector)")]
        [SerializeField] private MonoBehaviour eventBusProvider; // GameEventBus 보관 오브젝트(또는 EventLayerContext)
        [SerializeField] private BattleLoopExecutor executor;

        private BattleModifierComposeMediator _mediator;

        private void Awake()
        {
            IGameEventBus bus = null;

            if (eventBusProvider is EventLayerContext context)
            {
                context.EnsureInitialized();
                bus = context.Bus;
            }
            else
            {
                bus = eventBusProvider as IGameEventBus;
            }

            if (bus == null)
            {
                Debug.LogError("[Step7] eventBusProvider must be EventLayerContext or IGameEventBus provider.");
                enabled = false;
                return;
            }

            var step7Bus = new Step7EventBusAdapter(bus);

            executor.Initialize(step7Bus);
            _mediator = new BattleModifierComposeMediator(step7Bus);

            Debug.Log("[Step7] Bootstrapped");
        }

        private void OnDestroy()
        {
            _mediator?.Dispose();
        }
    }
}
