using UnityEngine;

namespace BossRaid.Gameplay.Promotion
{
    /// <summary>
    /// Promotion = 플레이 스타일 보정자(Modifier Provider)
    /// 고정 필드:
    /// - promotionId
    /// - weaponFamilyId (후보 생성 기준)
    /// - effectKeys[] (룰 테이블 해석 키)
    /// - modifiers(optional) (단순 수치 보정)
    /// </summary>
    [CreateAssetMenu(menuName = "BossRaid/Promotion/PromotionDefinition", fileName = "PromotionDefinitionSO")]
    public sealed class PromotionDefinitionSO : ScriptableObject
    {
        public string promotionId;
        public string weaponFamilyId;

        [Header("Effect Keys (Rule Table Input)")]
        public string[] effectKeys;

        [Header("Optional Numeric Modifiers")]
        public PromotionModifier[] modifiers;
    }
}