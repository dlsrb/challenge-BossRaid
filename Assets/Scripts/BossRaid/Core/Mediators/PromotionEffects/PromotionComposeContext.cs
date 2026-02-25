using BossRaid.Gameplay.Promotion;

namespace BossRaid.Core.Mediators.PromotionEffects
{
    /// <summary>
    /// Weapon + Promotion 조합 중간 컨텍스트.
    /// Step6에서는 "수치/키"만 다룬다.
    /// </summary>
    public struct PromotionComposeContext
    {
        public string weaponId;
        public string weaponFamilyId;

        public string promotionId;
        public string[] effectKeys;
        public PromotionModifier[] modifiers;

        // WeaponDefinition에서 가져온 기본값(예시: 최소한만)
        public float damage;
        public float cooldownSeconds;
    }
}