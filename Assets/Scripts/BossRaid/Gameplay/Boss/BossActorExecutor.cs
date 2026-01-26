using BossRaid.Core.Commands;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Command;
using BossRaid.Core.Events.Occurred;
using BossRaid.Core.Events.Requested;
using UnityEngine;

namespace BossRaid.Gameplay.Boss
{
    public sealed class BossActorExecutor : MonoBehaviour,
        IGameEventHandler<ActorCommandIssued>,   // ✅ 제네릭 핸들러
        IActorExecutor
    {
        [SerializeField] private string actorId = "Boss_001";
        [SerializeField] private Animator animator;

        private GameEventBus _bus;

        public void Init(GameEventBus bus) => _bus = bus;

        private void Start()
        {
            if (_bus == null) return;

            _bus.Publish(new ActorActionRequested(
                sourceId: actorId,
                actorId: actorId,
                actionId: "Attack"
            ));
        }

        // ✅ Handle 시그니처 변경
        public void Handle(ActorCommandIssued issued)
        {
            if (_bus == null) return;
            if (issued.TargetActorId != actorId) return;

            issued.Command.ExecuteOn(this);

            _bus.Publish(new ActorActionOccurred(
                sourceId: actorId,
                actorId: actorId,
                actionId: issued.Command.CommandId
            ));
        }

        public void Execute(PlayAnimationCommand command)
        {
            if (animator == null)
            {
                Debug.LogWarning($"[{actorId}] Animator not assigned. Cmd={command}");
                return;
            }
            animator.SetTrigger(command.TriggerName);
        }
    }
}
