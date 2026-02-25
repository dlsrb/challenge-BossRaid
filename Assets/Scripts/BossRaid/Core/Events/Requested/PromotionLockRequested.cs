using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Requested
{
    /// <summary>
    /// 전투 시작 등 "이제부터 전직 변경 금지" 요청.
    /// 실제로 잠글지 여부는 Mediator가 판단/전환한다.
    /// </summary>
    public sealed class PromotionLockRequested : GameEventBase
    {
        public override string EventName => nameof(PromotionLockRequested);

        public PromotionLockRequested(string sourceId) : base(sourceId) { }
    }
}
