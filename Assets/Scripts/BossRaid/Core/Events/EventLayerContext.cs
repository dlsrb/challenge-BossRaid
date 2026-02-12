using UnityEngine;

namespace BossRaid.Core.Events
{
    /// <summary>
    /// Scene/Battle 컨텍스트.
    /// 이 오브젝트가 EventBus 인스턴스를 생성/보관한다.
    /// </summary>
    public sealed class EventLayerContext : MonoBehaviour
    {
        public GameEventBus Bus { get; private set; }

        [SerializeField] private bool enableDebugLog = true;

        private void Awake()
        {
            EnsureInitialized();
        }

        public void EnsureInitialized()
        {
            if (Bus != null) return;

            IEventLogger logger = enableDebugLog ? new UnityDebugEventLogger() : null;
            Bus = new GameEventBus(logger);
        }
    }
}
