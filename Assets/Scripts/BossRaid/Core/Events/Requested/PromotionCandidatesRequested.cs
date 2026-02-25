using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Requested
{
    /// <summary>
    /// UI/상위 흐름이 "현재 무기 계열 기준 전직 후보를 보여달라"는 요청.
    /// UI는 표시만. 유효성 판단은 Mediator.
    /// </summary>
    public sealed class PromotionCandidatesRequested : GameEventBase
    {
        public override string EventName => nameof(PromotionCandidatesRequested);

        public string WeaponFamilyId { get; }

        public PromotionCandidatesRequested(string sourceId, string weaponFamilyId) : base(sourceId)
        {
            WeaponFamilyId = weaponFamilyId;
        }
    }
}
