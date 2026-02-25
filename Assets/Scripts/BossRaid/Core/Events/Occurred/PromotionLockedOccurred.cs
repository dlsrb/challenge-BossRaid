using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Occurred
{
    public sealed class PromotionLockedOccurred : GameEventBase
    {
        public override string EventName => nameof(PromotionLockedOccurred);

        public PromotionLockedOccurred(string sourceId) : base(sourceId) { }
    }
}
