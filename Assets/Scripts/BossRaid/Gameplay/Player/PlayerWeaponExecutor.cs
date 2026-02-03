// Assets/Scripts/BossRaid/Gameplay/Player/PlayerWeaponExecutor.cs
using UnityEngine;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Command;

namespace BossRaid.Gameplay.Player
{
    // 실행자는 "명령을 실행"만 한다. 판단/규칙 금지.
    public sealed class PlayerWeaponExecutor : MonoBehaviour, IGameEventHandler<WeaponAttackCommandIssued>
    {
        [SerializeField] private string actorId = "Player";
        [SerializeField] private Animator animator; // 선택

        private GameEventBus _bus;

        public void Init(GameEventBus bus)
        {
            _bus = bus;
        }

        public void Handle(WeaponAttackCommandIssued e)
        {
            // 이 실행자가 대상인지 확인(필터링은 OK: 규칙판단 아님, 라우팅/주소 처리)
            if (e.ActorId != actorId) return;

            // 1) 로그(흐름 추적)
            Debug.Log($"[PlayerWeaponExecutor] Execute Attack | weapon={e.Command.WeaponId}, dmg={e.Command.Damage}, special={e.Command.SpecialKey}");

            // 2) 애니 트리거(있으면 실행, 없으면 스킵)
            if (animator != null && !string.IsNullOrEmpty(e.Command.AnimTrigger))
            {
                animator.SetTrigger(e.Command.AnimTrigger);
            }

            // Step 5: 여기까지가 "실행" 최소.
            // 피격 판정/투사체 생성/데미지 적용은 Step 6~7 코어루프에서 붙인다.
        }
    }
}
