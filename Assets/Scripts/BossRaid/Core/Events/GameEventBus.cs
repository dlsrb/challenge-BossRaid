using System;
using System.Collections.Generic;

namespace BossRaid.Core.Events
{
    /// <summary>
    /// Event Layer: 수집/전달/기록만 담당.
    /// - 판정/계산/상태변경/의미변형 금지
    /// </summary>
    public sealed class GameEventBus
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
            list.Add(handler);
        }

        public void Unregister<TEvent>(IGameEventHandler<TEvent> handler) where TEvent : IGameEvent
        {
            var type = typeof(TEvent);
            if (_handlers.TryGetValue(type, out var list))
            {
                list.Remove(handler);
            }
        }

        public void Publish<TEvent>(TEvent gameEvent) where TEvent : IGameEvent
        {
            // 1) 기록(사실만)
            _logger?.Log(gameEvent);

            // 2) 전달
            var type = typeof(TEvent);
            if (!_handlers.TryGetValue(type, out var list)) return;

            for (int i = 0; i < list.Count; i++)
            {
                ((IGameEventHandler<TEvent>)list[i]).Handle(gameEvent);
            }
        }
    }
}
