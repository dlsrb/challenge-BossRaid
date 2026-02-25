namespace BossRaid.Core.Mediators.PromotionEffects
{
    /// <summary>
    /// effectKey 기반 룰 해석 단위.
    /// - 판단/실행이 아니라 "payload 보정"만 허용.
    /// - 새 효과 추가는 Rule 클래스를 추가하고 Registry에 등록.
    /// </summary>
    public interface IPromotionEffectRule
    {
        string EffectKey { get; }

        void Apply(ref PromotionComposeContext ctx);
    }
}