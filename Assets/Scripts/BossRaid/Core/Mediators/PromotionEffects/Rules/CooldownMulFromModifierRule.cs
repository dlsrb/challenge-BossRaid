using UnityEngine;

namespace BossRaid.Core.Mediators.PromotionEffects.Rules
{
    /// <summary>
    /// effectKey: effect.cooldown.mul
    /// modifiers 중 stat.cooldown.mul 값을 찾아 cooldown *= value 적용.
    /// </summary>
    public sealed class CooldownMulFromModifierRule : IPromotionEffectRule
    {
        public string EffectKey => "effect.cooldown.mul";

        public void Apply(ref PromotionComposeContext ctx)
        {
            var mods = ctx.modifiers;
            if (mods == null) return;

            for (int i = 0; i < mods.Length; i++)
            {
                if (mods[i].statKey == "stat.cooldown.mul")
                {
                    ctx.cooldownSeconds = Mathf.Max(0.05f, ctx.cooldownSeconds * mods[i].value);
                }
            }
        }
    }
}