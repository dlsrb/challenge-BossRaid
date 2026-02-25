// Assets/Scripts/BossRaid/Battle/Core/Events/GameEventBusExtensions.cs
using System;

namespace BossRaid.Core.Events
{
    /// <summary>
    /// 레거시/튜토리얼 호환용 확장 메서드.
    /// - 일부 Step 코드에서 bus.Raise(e) 형태를 쓰는 경우가 있어 제공한다.
    /// - 프로젝트 표준은 bus.Publish(...) 이다.
    /// </summary>
    public static class GameEventBusExtensions
    {
        /// <summary>
        /// 레거시 Step 코드 호환용.
        /// 가능하면 Raise 대신 Publish를 사용한다.
        /// </summary>
        public static void Raise<TEvent>(this IGameEventBus bus, TEvent gameEvent)
            where TEvent : IGameEvent
        {
            if (bus == null) throw new ArgumentNullException(nameof(bus));
            if (gameEvent == null) throw new ArgumentNullException(nameof(gameEvent));

            bus.Publish(gameEvent);
        }

        public static void Raise(this IGameEventBus bus, IGameEvent gameEvent)
        {
            throw new NotSupportedException(
                "Do not call Raise(IGameEvent). Use bus.Publish(concreteEvent) or Raise<TEvent>(concreteEvent)."
            );
        }
    }
}
