namespace BossRaid.Core.Commands
{
    public abstract class ActorCommandBase : IActorCommand
    {
        public abstract string CommandId { get; }
        public abstract void ExecuteOn(IActorExecutor executor);

        public override string ToString() => CommandId;
    }
}
