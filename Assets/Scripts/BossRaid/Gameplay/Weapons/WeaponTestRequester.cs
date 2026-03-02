// Assets/Scripts/BossRaid/Gameplay/Weapons/WeaponTestRequester.cs
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

using BossRaid.Core.Events;
using BossRaid.Core.Events.Requested;

namespace BossRaid.Gameplay.Weapons
{
    /// <summary>
    /// Step 5 디버그 도구: ContextMenu로 Requested 이벤트를 발행한다.
    /// </summary>
    public sealed class WeaponTestRequester : MonoBehaviour
    {
        [Header("Bus (EventLayerContext in Scene)")]
        [SerializeField] private EventLayerContext eventLayerContext;

        [Header("Target Actor")]
        [SerializeField] private string actorId = "Player";

        [Header("Equip Test")]
        [SerializeField] private WeaponDefinitionSO weaponToEquip;

        [Header("Use Test")]
        [SerializeField] private Vector2 testAim = Vector2.right;

        [ContextMenu("Test/Weapon Equip Requested")]
        public void TestEquipRequested()
        {
            if (!TryGetBus(out var bus)) return;

            if (weaponToEquip == null)
            {
                Debug.LogError("[WeaponTestRequester] weaponToEquip is not assigned (set a WeaponDefinitionSO)");
                return;
            }

            var spec = new WeaponEquipRequested.WeaponSpec(
                weaponId: weaponToEquip.WeaponId,
                baseDamage: weaponToEquip.BaseDamage,
                cooldownSeconds: weaponToEquip.CooldownSeconds,
                attackAnimTrigger: weaponToEquip.AttackAnimTrigger,
                specialKey: weaponToEquip.SpecialKey
            );

            var e = new WeaponEquipRequested(
                sourceId: "WeaponTestRequester",
                actorId: actorId,
                weapon: spec
            );

            bus.Publish(e);

            Debug.Log($"[WeaponTestRequester] EquipRequested fired | actor={actorId}, weapon={weaponToEquip.WeaponId}");
        }

        [ContextMenu("Test/Weapon Use Requested")]
        public void TestUseRequested()
        {
            if (!TryGetBus(out var bus)) return;

            var e = new WeaponUseRequested(
                sourceId: "WeaponTestRequester",
                actorId: actorId
            );

            bus.Publish(e);

            Debug.Log($"[WeaponTestRequester] UseRequested fired | actor={actorId}, aim={testAim}");
        }

        [ContextMenu("Test/Equip + Use Requested")]
        public void TestEquipAndUse()
        {
            TestEquipRequested();
            TestUseRequested();
        }

        private bool TryGetBus(out GameEventBus bus)
        {
            bus = null;

            if (eventLayerContext == null)
            {
                Debug.LogError("[WeaponTestRequester] eventLayerContext is not assigned (link EventLayerContext in the Inspector)");
                return false;
            }

            // ContextMenu can be invoked outside Play Mode, so initialize lazily.
            eventLayerContext.EnsureInitialized();

            if (eventLayerContext.Bus == null)
            {
                Debug.LogError("[WeaponTestRequester] eventLayerContext.Bus is null (initialization order)");
                return false;
            }

            bus = eventLayerContext.Bus;
            return true;
            
        }
    }
}
[CustomEditor(typeof(BossRaid.Gameplay.Weapons.WeaponTestRequester))]
public sealed class WeaponTestRequesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 Inspector(기존 필드들) 그대로 표시
        DrawDefaultInspector();

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Step5 Debug Buttons", EditorStyles.boldLabel);

        var t = (BossRaid.Gameplay.Weapons.WeaponTestRequester)target;

        // Play Mode가 아닐 때도 ContextMenu처럼 호출 가능(너 TryGetBus가 lazy init 하니까 OK)
        if (GUILayout.Button("Test: Weapon Equip Requested"))
            t.TestEquipRequested();

        if (GUILayout.Button("Test: Weapon Use Requested"))
            t.TestUseRequested();

        if (GUILayout.Button("Test: Equip + Use Requested"))
            t.TestEquipAndUse();
    }
}
#endif
