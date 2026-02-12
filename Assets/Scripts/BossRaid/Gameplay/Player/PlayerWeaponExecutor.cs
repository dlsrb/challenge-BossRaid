// Assets/Scripts/BossRaid/Gameplay/Player/PlayerWeaponExecutor.cs
using UnityEngine;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Command;

namespace BossRaid.Gameplay.Player
{
    // Executor는 "실행"만 한다. 판단/규칙(Decision)은 Mediator에서만 한다.
    public sealed class PlayerWeaponExecutor : MonoBehaviour, IGameEventHandler<WeaponAttackCommandIssued>
    {
        [SerializeField] private string actorId = "Player";
        [SerializeField] private Animator animator;

        private GameEventBus _bus;

        public void Init(GameEventBus bus)
        {
            _bus = bus;
        }

        public void Handle(WeaponAttackCommandIssued e)
        {
            // 이 Executor가 담당하는 Actor인지 필터링(규칙 판단이 아니라 라우팅/대상 확인).
            if (e.ActorId != actorId) return;

            // 1) 로그(흐름 추적)
            Debug.Log($"[PlayerWeaponExecutor] Execute Attack | weapon={e.Command.WeaponId}, dmg={e.Command.Damage}, special={e.Command.SpecialKey}");

            // 2) 실행(연출/애니메이션)
            if (animator != null && !string.IsNullOrEmpty(e.Command.AnimTrigger))
            {
                animator.SetTrigger(e.Command.AnimTrigger);
            }

            // 실제 데미지/히트 판정/스폰 등은 Step 6~7에서 확장한다.
        }
    }
}
