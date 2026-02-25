using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Occurred
{
    /// <summary>
    /// Mediator가 weaponFamilyId에 대해 제공 가능한 후보 목록을 제공(표시용).
    /// - 잠금/해금/유효성 판단은 이미 Mediator에서 반영된 결과여야 한다.
    /// </summary>
    public sealed class PromotionCandidatesProvidedOccurred : GameEventBase
    {
        public override string EventName => nameof(PromotionCandidatesProvidedOccurred);

        public string WeaponFamilyId { get; }
        public string[] CandidatePromotionIds { get; }

        public PromotionCandidatesProvidedOccurred(string sourceId, string weaponFamilyId, string[] candidatePromotionIds) : base(sourceId)
        {
            WeaponFamilyId = weaponFamilyId;
            CandidatePromotionIds = candidatePromotionIds;
        }
    }
}
