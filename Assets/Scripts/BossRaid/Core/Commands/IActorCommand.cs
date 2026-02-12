namespace BossRaid.Core.Commands
{
    // Actor에게 실행시킬 명령(Command).
    public interface IActorCommand
    {
        string CommandId { get; }
        void ExecuteOn(IActorExecutor executor);
    }

    // Actor가 받을 수 있는 실행 인터페이스(더블 디스패치용).
    // Command 종류가 늘어나면 Execute(새Command)만 추가한다(Executor 내부 if/switch를 피하기 위함).
    public interface IActorExecutor
    {
        void Execute(PlayAnimationCommand command);
    }
}
