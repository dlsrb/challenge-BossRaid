using System;

namespace BossRaid.Core.Events
{
    public interface IGameEvent
    {
        DateTime UtcTime { get; }
        string SourceId { get; }   // "Player", "Boss" 같은 식별자 (구체 참조 금지)
        string EventName { get; }  // 타입 이름/로그용
    }
}
