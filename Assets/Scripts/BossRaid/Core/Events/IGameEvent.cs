using System;

namespace BossRaid.Core.Events
{
    public interface IGameEvent
    {
        DateTime UtcTime { get; }
        string SourceId { get; }   // "Player", "Boss" 같은 발행 주체 식별자(디버깅/로그용)
        string EventName { get; }  // 로그/디버깅용 이벤트 이름
    }
}
