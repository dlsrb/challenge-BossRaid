using BossRaid.Core.Commands;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Command;
using BossRaid.Core.Events.Requested;

namespace BossRaid.Core.Mediators
{
    // ✅ 제네릭 핸들러로 변경
    public sealed class BattleMediator : IGameEventHandler<ActorActionRequested>
    {
        private readonly IGameEventBus _bus;
        private readonly string _mediatorId;

        public BattleMediator(IGameEventBus bus, string mediatorId = "Mediator")
        {
            _bus = bus;
            _mediatorId = mediatorId;
        }

        // ✅ Handle 시그니처 변경
        public void Handle(ActorActionRequested req)
        {
            var cmd = new PlayAnimationCommand("Attack");

            _bus.Publish(new ActorCommandIssued(
                sourceId: _mediatorId,
                targetActorId: req.ActorId,
                command: cmd
            ));
        }
    }
}
