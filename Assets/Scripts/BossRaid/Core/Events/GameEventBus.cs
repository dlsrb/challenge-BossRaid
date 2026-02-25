using System;
using System.Collections.Generic;

namespace BossRaid.Core.Events
{
    /// <summary>
    /// Event Layer: 이벤트 전달 + (옵션) 기록.
    /// - 이 레이어는 '전달'과 '기록'만 한다.
    /// - 판단/규칙(Decision)은 Mediator에서만 한다.
    /// </summary>
    public sealed class GameEventBus : IGameEventBus
    {
        private readonly IEventLogger _logger;
        private readonly Dictionary<Type, List<object>> _handlers = new Dictionary<Type, List<object>>();

        public GameEventBus(IEventLogger logger = null)
        {
            _logger = logger;
        }

        public void Register<TEvent>(IGameEventHandler<TEvent> handler) where TEvent : IGameEvent
        {
            var type = typeof(TEvent);
            if (!_handlers.TryGetValue(type, out var list))
            {
                list = new List<object>();
                _handlers[type] = list;
            }

            // 중복 등록 방지(같은 인스턴스).
            for (int i = 0; i < list.Count; i++)
            {
                if (ReferenceEquals(list[i], handler))
                    return;
            }

            list.Add(handler);
        }

        public void Unregister<TEvent>(IGameEventHandler<TEvent> handler) where TEvent : IGameEvent
        {
            var type = typeof(TEvent);
            if (_handlers.TryGetValue(type, out var list))
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (ReferenceEquals(list[i], handler))
                        list.RemoveAt(i);
                }
            }
        }

        public void Publish<TEvent>(TEvent gameEvent) where TEvent : IGameEvent
        {
            // 1) 기록(옵션)
            _logger?.Log(gameEvent);

            // 2) 전달
            var type = typeof(TEvent);
            if (!_handlers.TryGetValue(type, out var list)) return;

            // 전달 중 register/unregister가 발생해도 안전하도록 스냅샷으로 순회.
            var snapshot = list.ToArray();
            for (int i = 0; i < snapshot.Length; i++)
            {
                ((IGameEventHandler<TEvent>)snapshot[i]).Handle(gameEvent);
            }
        }
    }
}
