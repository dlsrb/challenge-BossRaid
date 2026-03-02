using System.Collections;
using UnityEngine;
using BossRaid.Core.Loop;
using BossRaid.Core.Loop.Requests;
using BossRaid.Core.Events; // IGameEventBus
using BossRaid.Core.Events; // EventLayerContext 네임스페이스가 여기 아니면 수정

namespace BossRaid.Battle.Loop
{
    public sealed class Step7DebugInput : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour eventBusProvider; // EventLayerContext 넣기
        [SerializeField] private string actorId = "player.1";

        private IStep7EventBus _bus;

        private IEnumerator Start()
        {
            // ✅ 한 프레임 양보: 다른 컴포넌트 Awake/초기화가 먼저 끝나게 함
            yield return null;

            // 1) EventLayerContext인 경우: Bus가 준비될 때까지 1~2프레임 더 기다려본다(안전)
            if (eventBusProvider is EventLayerContext ctx)
            {
                // Bus가 늦게 세팅되는 케이스 방어
                for (int i = 0; i < 3 && ctx.Bus == null; i++)
                    yield return null;

                if (ctx.Bus == null)
                {
                    Debug.LogError("[Step7] DebugInput: EventLayerContext.Bus is null. " +
                                   "EventLayerContext 초기화 타이밍/EnsureInitialized 필요.");
                    enabled = false;
                    yield break;
                }

                _bus = new Step7EventBusAdapter(ctx.Bus);
                Debug.Log("[Step7] DebugInput ready (EventLayerContext.Bus)");
                yield break;
            }

            // 2) IGameEventBus가 직접 들어오는 경우
            if (eventBusProvider is IGameEventBus rawBus)
            {
                _bus = new Step7EventBusAdapter(rawBus);
                Debug.Log("[Step7] DebugInput ready (IGameEventBus)");
                yield break;
            }

            Debug.LogError("[Step7] DebugInput: provider is neither EventLayerContext nor IGameEventBus. Fix inspector wiring.");
            enabled = false;
        }

        private void Update()
        {
            if (_bus == null) return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("[Step7] Input E -> BattleEnterRequested");
                _bus.Publish(new BattleEnterRequested(actorId));
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("[Step7] Input Space -> PlayerAttackRequested");
                _bus.Publish(new PlayerAttackRequested(actorId));
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("[Step7] Input R -> BattleRetryRequested");
                _bus.Publish(new BattleRetryRequested(actorId));
            }
        }
    }
}