namespace BossRaid.Core.Commands
{
    // 타입 분기 없이 확장 가능한 Command 계약
    public interface IActorCommand
    {
        string CommandId { get; }
        void ExecuteOn(IActorExecutor executor);
    }

    // Actor(실행자)가 Command를 수행하기 위한 계약
    // Command가 늘면 Execute(새Command)만 추가 (if/switch 금지)
    public interface IActorExecutor
    {
        void Execute(PlayAnimationCommand command);
    }
}
