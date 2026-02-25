namespace BossRaid.Core.Mediators.PromotionEffects.Rules
{
    /// <summary>
    /// effectKey: effect.damage.mul
    /// modifiers 중 stat.damage.mul 값을 찾아 damage *= value 적용.
    /// </summary>
    public sealed class DamageMulFromModifierRule : IPromotionEffectRule
    {
        public string EffectKey => "effect.damage.mul";

        public void Apply(ref PromotionComposeContext ctx)
        {
            var mods = ctx.modifiers;
            if (mods == null) return;

            for (int i = 0; i < mods.Length; i++)
            {
                if (mods[i].statKey == "stat.damage.mul")
                {
                    ctx.damage *= mods[i].value;
                }
            }
        }
    }
}