using UnityEngine;

namespace BossRaid.Core.Events
{
    /// <summary>
    /// Battle/SceneContext 소속.
    /// 씬 범위에서 EventBus를 생성/보관한다.
    /// (전역 싱글톤 강제하지 않음)
    /// </summary>
    public sealed class EventLayerContext : MonoBehaviour
    {
        public GameEventBus Bus { get; private set; }

        [SerializeField] private bool enableDebugLog = true;

        private void Awake()
        {
            IEventLogger logger = enableDebugLog ? new UnityDebugEventLogger() : null;
            Bus = new GameEventBus(logger);
        }
    }
}
