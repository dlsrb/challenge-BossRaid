using System.Collections.Generic;

namespace BossRaid.Core.Mediators.PromotionEffects
{
    /// <summary>
    /// effectKey -> Rule 매핑(룰 테이블).
    /// - promotionId 기반 if/switch 금지
    /// - effectKey는 데이터 입력, 해석은 Rule 객체로 위임
    /// </summary>
    public sealed class PromotionEffectRuleRegistry
    {
        private readonly Dictionary<string, IPromotionEffectRule> _byKey = new Dictionary<string, IPromotionEffectRule>();

        public PromotionEffectRuleRegistry(IEnumerable<IPromotionEffectRule> rules)
        {
            foreach (var r in rules)
            {
                if (r == null) continue;
                if (string.IsNullOrEmpty(r.EffectKey)) continue;
                _byKey[r.EffectKey] = r;
            }
        }

        public bool TryGet(string effectKey, out IPromotionEffectRule rule)
        {
            if (string.IsNullOrEmpty(effectKey))
            {
                rule = null;
                return false;
            }
            return _byKey.TryGetValue(effectKey, out rule);
        }
    }
}