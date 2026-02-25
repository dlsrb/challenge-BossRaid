using System;
using UnityEngine;

namespace BossRaid.Gameplay.Promotion
{
    /// <summary>
    /// Promotion이 제공하는 "단순 수치 보정" (optional).
    /// - Promotion은 판단/실행 금지
    /// - Mediator가 조합할 때만 읽는다.
    /// </summary>
    [Serializable]
    public struct PromotionModifier
    {
        [Tooltip("예: stat.damage.mul, stat.cooldown.mul, stat.damage.add")]
        public string statKey;

        [Tooltip("mul: 1.2 = 20% 증가 / add: +1 같은 값도 float로 표현")]
        public float value;
    }
}