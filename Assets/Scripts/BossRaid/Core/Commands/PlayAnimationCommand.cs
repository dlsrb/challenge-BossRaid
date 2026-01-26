namespace BossRaid.Core.Commands
{
    // Step4 최소 동작: 애니메이션 트리거 실행
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
            executor.Execute(this); // 더블 디스패치 (타입 분기 없음)
        }

        public override string ToString()
            => $"{CommandId}(Trigger={TriggerName})";
    }
}
