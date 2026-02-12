using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using BossRaid.Core.Events;

namespace BossRaid.Gameplay.Weapons
{
    // 디버그 도구: GameEventBus 메서드를 리플렉션으로 조회한다.
    public sealed class GameEventBusProbe : MonoBehaviour
    {
        [SerializeField] private EventLayerContext eventLayerContext;

        [ContextMenu("Probe/GameEventBus Methods")]
        public void ProbeMethods()
        {
            if (eventLayerContext == null)
            {
                Debug.LogError("[GameEventBusProbe] EventLayerContext is null (check scene wiring)");
                return;
            }

            // ContextMenu can be invoked outside Play Mode, so initialize lazily.
            eventLayerContext.EnsureInitialized();

            if (eventLayerContext.Bus == null)
            {
                Debug.LogError("[GameEventBusProbe] Bus is null (initialization failed)");
                return;
            }

            var bus = eventLayerContext.Bus;
            var busType = bus.GetType();

            // 1) 모든 인스턴스 메서드 나열
            var methods = busType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            Debug.Log($"[GameEventBusProbe] BusType = {busType.FullName}");
            Debug.Log($"[GameEventBusProbe] Total Methods = {methods.Length}");

            // 2) IGameEvent 계열 1-파라미터 메서드 후보 찾기
            var candidates = methods
                .Where(m => m.GetParameters().Length == 1)
                .Select(m => new
                {
                    Method = m,
                    Param = m.GetParameters()[0].ParameterType
                })
                .Where(x =>
                    x.Param == typeof(IGameEvent) ||
                    typeof(IGameEvent).IsAssignableFrom(x.Param) ||
                    // 최후 후보
                    x.Param == typeof(object)
                )
                .ToList();

            if (candidates.Count == 0)
            {
                Debug.LogWarning("[GameEventBusProbe] No 1-parameter IGameEvent-like methods found.");
            }
            else
            {
                Debug.Log($"[GameEventBusProbe] 1-parameter candidates = {candidates.Count}");
                foreach (var c in candidates)
                {
                    Debug.Log($"[Candidate] {c.Method.Name}({c.Param.FullName})  access={(c.Method.IsPublic ? "public" : "non-public")}");
                }
            }

            // 3) Register 계열 메서드도 함께 표시
            var registerLike = methods
                .Where(m => m.Name.IndexOf("Register", StringComparison.OrdinalIgnoreCase) >= 0)
                .ToArray();

            if (registerLike.Length > 0)
            {
                Debug.Log($"[GameEventBusProbe] Register-like methods = {registerLike.Length}");
                foreach (var m in registerLike)
                {
                    var ps = m.GetParameters();
                    var sig = string.Join(", ", ps.Select(p => p.ParameterType.Name));
                    Debug.Log($"[RegisterLike] {m.Name}({sig})");
                }
            }
        }
    }
}
