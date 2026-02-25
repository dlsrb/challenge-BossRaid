using BossRaid.Core.Events;

namespace BossRaid.Core.Events.Occurred
{
    /// <summary>
    /// WeaponDefinition + Promotion 조합 결과 컨텍스트(검증/로깅용).
    /// Step6에서는 Executor가 이걸로 실행하지 않는다(구조 수정 최소).
    /// </summary>
    public sealed class PromotionContextOccurred : GameEventBase
    {
        public override string EventName => nameof(PromotionContextOccurred);

        public string WeaponId { get; }
        public string WeaponFamilyId { get; }
        public string PromotionId { get; }
        public string[] EffectKeys { get; }

        public PromotionContextOccurred(string sourceId, string weaponId, string weaponFamilyId, string promotionId, string[] effectKeys)
            : base(sourceId)
        {
            WeaponId = weaponId;
            WeaponFamilyId = weaponFamilyId;
            PromotionId = promotionId;
            EffectKeys = effectKeys;
        }
    }
}
