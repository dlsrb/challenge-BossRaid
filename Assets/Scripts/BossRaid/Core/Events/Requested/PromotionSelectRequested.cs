using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Requested
{
    /// <summary>
    /// 전직 선택 의도(요청). 가능/불가는 Mediator가 판단한다.
    /// </summary>
    public sealed class PromotionSelectRequested : GameEventBase
    {
        public override string EventName => nameof(PromotionSelectRequested);

        public string PromotionId { get; }

        public PromotionSelectRequested(string sourceId, string promotionId) : base(sourceId)
        {
            PromotionId = promotionId;
        }
    }
}
