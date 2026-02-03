// Assets/Scripts/BossRaid/Gameplay/Weapons/WeaponTestRequester.cs
using UnityEngine;

using BossRaid.Core.Events;
using BossRaid.Core.Events.Requested;
using BossRaid.Gameplay.Weapons;

namespace BossRaid.Gameplay.Weapons
{
    /// <summary>
    /// Step 5 테스트용: 플레이 중에 Requested 이벤트를 손으로 발행한다.
    /// - 판단 없음(그냥 요청 발생)
    /// - EventLayerContext.Bus의 Publish를 사용
    /// </summary>
    public sealed class WeaponTestRequester : MonoBehaviour
    {
        [Header("Bus (SceneContext/EventLayer의 EventLayerContext 연결)")]
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
                Debug.LogError("[WeaponTestRequester] weaponToEquip 이 비어있음 (WeaponDefinitionSO 에셋 연결 필요)");
                return;
            }

            var e = new WeaponEquipRequested(
                sourceId: "WeaponTestRequester",
                actorId: actorId,
                weapon: weaponToEquip
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
                Debug.LogError("[WeaponTestRequester] eventLayerContext가 비어있음 (SceneContext/EventLayer의 EventLayerContext를 연결)");
                return false;
            }

            if (eventLayerContext.Bus == null)
            {
                Debug.LogError("[WeaponTestRequester] eventLayerContext.Bus가 null (Awake 실행 전일 수 있음)");
                return false;
            }

            bus = eventLayerContext.Bus;
            return true;
        }
    }
}
