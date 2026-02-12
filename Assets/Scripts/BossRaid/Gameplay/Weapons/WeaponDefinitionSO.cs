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
        [Tooltip("Key interpreted by the mediator (rules layer).")]
        [SerializeField] private string conceptKey = "concept.melee";

        [Tooltip("Search/filter tags (e.g. Melee, Magic, DoT, Burst).")]
        [SerializeField] private string[] tags;

        [Header("Balance (you tune later)")]
        [SerializeField] private float baseDamage = 10f;
        [SerializeField] private float cooldownSeconds = 0.5f;

        [Header("Presentation Hooks (executor uses, no decision)")]
        [Tooltip("Animator Trigger name (used by the executor).")]
        [SerializeField] private string attackAnimTrigger = "Attack";

        [Tooltip("Optional attack prefab reference.")]
        [SerializeField] private GameObject attackPrefab;

        [Header("Special (you design later)")]
        [Tooltip("Special option key (e.g. special.chain, special.explode).")]
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
