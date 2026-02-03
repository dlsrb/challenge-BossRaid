// Assets/Scripts/BossRaid/Battle/Core/Events/GameEventBusExtensions.cs
using System;
using System.Reflection;

namespace BossRaid.Core.Events
{
    /// <summary>
    /// 프로젝트마다 GameEventBus의 "발행 메서드 이름"이 다를 수 있어서,
    /// Step 코드에서 bus.Raise(event)를 고정으로 쓰기 위한 호환 레이어.
    ///
    /// 기존 GameEventBus.cs는 수정하지 않는다(추가만).
    /// </summary>
    public static class GameEventBusExtensions
    {
        // 자주 쓰는 후보 이름들 (네 GameEventBus에 있는 이름 1개만 걸리면 됨)
        private static readonly string[] CandidateMethodNames =
        {
            "Raise",
            "Publish",
            "Emit",
            "Send",
            "Dispatch",
            "Post",
            "Fire",
        };

        /// <summary>
        /// Step 코드에서는 이 메서드만 사용.
        /// 내부에서 실제 GameEventBus의 발행 메서드를 찾아 호출한다.
        /// </summary>
        public static void Raise(this GameEventBus bus, IGameEvent gameEvent)
        {
            if (bus == null) throw new ArgumentNullException(nameof(bus));
            if (gameEvent == null) throw new ArgumentNullException(nameof(gameEvent));

            var busType = bus.GetType();

            foreach (var name in CandidateMethodNames)
            {
                // (1) 파라미터 1개짜리 메서드 찾기
                var method = busType.GetMethod(
                    name,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                    binder: null,
                    types: new[] { gameEvent.GetType() },
                    modifiers: null
                );

                if (method != null)
                {
                    method.Invoke(bus, new object[] { gameEvent });
                    return;
                }

                // (2) 파라미터 타입이 IGameEvent인 버전도 시도
                method = busType.GetMethod(
                    name,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                    binder: null,
                    types: new[] { typeof(IGameEvent) },
                    modifiers: null
                );

                if (method != null)
                {
                    method.Invoke(bus, new object[] { gameEvent });
                    return;
                }
            }

            throw new MissingMethodException(
                $"GameEventBus에 이벤트 발행 메서드를 찾지 못했습니다. " +
                $"시도한 이름: {string.Join(", ", CandidateMethodNames)}"
            );
        }
    }
}
