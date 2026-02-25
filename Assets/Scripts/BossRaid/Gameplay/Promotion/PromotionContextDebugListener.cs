using UnityEngine;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Occurred;

namespace BossRaid.Gameplay.Promotion
{
    public sealed class PromotionContextDebugListener : MonoBehaviour,
        IGameEventHandler<PromotionContextOccurred>
    {
        [SerializeField] private EventLayerContext eventLayerContext;

        private GameEventBus _bus;

        private void OnEnable()
        {
            if (eventLayerContext == null)
            {
                Debug.LogError("[PromotionContextDebugListener] EventLayerContext missing.");
                return;
            }

            eventLayerContext.EnsureInitialized();
            _bus = eventLayerContext.Bus;

            _bus.Register<PromotionContextOccurred>(this);
        }

        private void OnDisable()
        {
            if (_bus == null) return;
            _bus.Unregister<PromotionContextOccurred>(this);
        }

        public void Handle(PromotionContextOccurred e)
        {
            var keys = (e.EffectKeys == null) ? "(null)" : string.Join(", ", e.EffectKeys);
            Debug.Log($"[PromotionContext] weapon={e.WeaponId} family={e.WeaponFamilyId} promo={e.PromotionId} keys=[{keys}]");
        }
    }
}