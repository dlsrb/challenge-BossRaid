// Assets/Scripts/BossRaid/Gameplay/Weapons/WeaponDefinitionSO.cs
using UnityEngine;

namespace BossRaid.Gameplay.Weapons
{
    [CreateAssetMenu(menuName = "BossRaid/Weapon/Weapon Definition", fileName = "WeaponDefinition_")]
    public sealed class WeaponDefinitionSO : ScriptableObject
    {
        [Header("Identity")]
        [SerializeField] private string weaponId = "weapon.default";
        [SerializeField] private string displayName = "Default Weapon";

        [Header("Concept (you fill only)")]
        [Tooltip("Mediator가 해석하는 키. if 분기 대신 키/태그로 확장.")]
        [SerializeField] private string conceptKey = "concept.melee";

        [Tooltip("검색/분류용 태그(예: Melee, Magic, Dot, Burst)")]
        [SerializeField] private string[] tags;

        [Header("Balance (you tune later)")]
        [SerializeField] private float baseDamage = 10f;
        [SerializeField] private float cooldownSeconds = 0.5f;

        [Header("Presentation Hooks (executor uses, no decision)")]
        [Tooltip("Animator Trigger 이름(없으면 실행자는 스킵)")]
        [SerializeField] private string attackAnimTrigger = "Attack";

        [Tooltip("공격 프리팹(필수 아님). Step 5에서는 생성 안 해도 됨.")]
        [SerializeField] private GameObject attackPrefab;

        [Header("Special (you design later)")]
        [Tooltip("특수능력/옵션 확장용 키(예: special.chain, special.explode)")]
        [SerializeField] private string specialKey = "";

        public string WeaponId => weaponId;
        public string DisplayName => displayName;
        public string ConceptKey => conceptKey;
        public string[] Tags => tags;

        public float BaseDamage => baseDamage;
        public float CooldownSeconds => cooldownSeconds;

        public string AttackAnimTrigger => attackAnimTrigger;
        public GameObject AttackPrefab => attackPrefab;

        public string SpecialKey => specialKey;
    }
}
