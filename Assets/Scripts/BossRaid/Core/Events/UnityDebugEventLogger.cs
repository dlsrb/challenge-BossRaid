using UnityEngine;

namespace BossRaid.Core.Events
{
    public sealed class UnityDebugEventLogger : IEventLogger
    {
        public void Log(IGameEvent gameEvent)
        {
            Debug.Log($"[Event] {gameEvent.UtcTime:O} | {gameEvent.SourceId} | {gameEvent.EventName}");
        }
    }
}
