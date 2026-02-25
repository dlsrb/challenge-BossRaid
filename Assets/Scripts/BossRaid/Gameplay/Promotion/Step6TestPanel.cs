using UnityEngine;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Requested;

namespace BossRaid.Gameplay.Promotion
{
    public sealed class Step6TestPanel : MonoBehaviour
    {
        [SerializeField] private EventLayerContext eventLayerContext;
        [SerializeField] private string sourceId = "Step6TestPanel";

        [Header("Manual Inputs")]
        [SerializeField] private string weaponFamilyId = "sword";
        [SerializeField] private string promotionId = "promo.sword.berserk";

        public void RequestCandidates()
        {
            var bus = eventLayerContext != null ? eventLayerContext.Bus : null;
            if (bus == null)
            {
                Debug.LogError("[Step6TestPanel] Bus is null. Wire EventLayerContext in Inspector.");
                return;
            }

            bus.Publish(new PromotionCandidatesRequested(sourceId, weaponFamilyId));
        }

        public void SelectPromotion()
        {
            var bus = eventLayerContext != null ? eventLayerContext.Bus : null;
            if (bus == null)
            {
                Debug.LogError("[Step6TestPanel] Bus is null. Wire EventLayerContext in Inspector.");
                return;
            }

            bus.Publish(new PromotionSelectRequested(sourceId, promotionId));
        }

        public void LockPromotion()
        {
            var bus = eventLayerContext != null ? eventLayerContext.Bus : null;
            if (bus == null)
            {
                Debug.LogError("[Step6TestPanel] Bus is null. Wire EventLayerContext in Inspector.");
                return;
            }

            bus.Publish(new PromotionLockRequested(sourceId));
        }
    }
}