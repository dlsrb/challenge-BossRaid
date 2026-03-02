using System;
using System.Collections.Generic;
using BossRaid.Core.Events;

namespace BossRaid.Core.Loop
{
    public sealed class Step7EventBusAdapter : IStep7EventBus
    {
        private readonly IGameEventBus _bus;
        private readonly Dictionary<(Type, Delegate), object> _subscriptions =
            new Dictionary<(Type, Delegate), object>();

        public Step7EventBusAdapter(IGameEventBus bus)
        {
            _bus = bus;
        }

        public void Publish<T>(T evt) where T : IGameEvent
        {
            _bus.Publish(evt);
        }

        public void Subscribe<T>(Action<T> handler) where T : IGameEvent
        {
            if (handler == null) return;

            var key = (typeof(T), (Delegate)handler);
            if (_subscriptions.ContainsKey(key)) return;

            var wrapper = new ActionEventHandler<T>(handler);
            _subscriptions[key] = wrapper;
            _bus.Register(wrapper);
        }

        public void Unsubscribe<T>(Action<T> handler) where T : IGameEvent
        {
            if (handler == null) return;

            var key = (typeof(T), (Delegate)handler);
            if (!_subscriptions.TryGetValue(key, out var wrapperObj)) return;
            if (wrapperObj is not ActionEventHandler<T> wrapper) return;

            _bus.Unregister(wrapper);
            _subscriptions.Remove(key);
        }

        private sealed class ActionEventHandler<T> : IGameEventHandler<T> where T : IGameEvent
        {
            private readonly Action<T> _handler;

            public ActionEventHandler(Action<T> handler)
            {
                _handler = handler;
            }

            public void Handle(T gameEvent)
            {
                _handler?.Invoke(gameEvent);
            }
        }
    }
}
