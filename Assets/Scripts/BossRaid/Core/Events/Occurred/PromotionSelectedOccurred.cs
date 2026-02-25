using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Occurred
{
    /// <summary>
    /// 전직 선택 확정(사실 보고).
    /// </summary>
    public sealed class PromotionSelectedOccurred : GameEventBase
    {
        public override string EventName => nameof(PromotionSelectedOccurred);

        public string PromotionId { get; }

        public PromotionSelectedOccurred(string sourceId, string promotionId) : base(sourceId)
        {
            PromotionId = promotionId;
        }
    }
}
