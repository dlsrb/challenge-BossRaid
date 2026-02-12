namespace BossRaid.Core.Commands
{
    // 애니메이션 트리거 실행 명령(Executor는 실행만 하고 판단하지 않는다).
    public sealed class PlayAnimationCommand : ActorCommandBase
    {
        public override string CommandId => "PlayAnimation";
        public string TriggerName { get; }

        public PlayAnimationCommand(string triggerName)
        {
            TriggerName = triggerName;
        }

        public override void ExecuteOn(IActorExecutor executor)
        {
            executor.Execute(this); // 더블 디스패치: 타입에 맞는 Execute가 호출된다.
        }

        public override string ToString()
            => $"{CommandId}(Trigger={TriggerName})";
    }
}
