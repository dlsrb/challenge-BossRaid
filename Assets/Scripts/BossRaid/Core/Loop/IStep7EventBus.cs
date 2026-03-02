// Assets/Scripts/BossRaid/Core/Loop/IStep7EventBus.cs
using System;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop
{
    /// <summary>
    /// Step7 전용 최소 버스 어댑터.
    /// 기존 IGameEventBus API가 뭐든, 여기로 맞춰 끼운다(추가만).
    /// </summary>
public interface IStep7EventBus
    {
        void Publish<T>(T evt) where T : IGameEvent;
        void Subscribe<T>(Action<T> handler) where T : IGameEvent;
        void Unsubscribe<T>(Action<T> handler) where T : IGameEvent;
    }
}