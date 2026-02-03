using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using BossRaid.Core.Events;

namespace BossRaid.Gameplay.Weapons
{
    // GameEventBus에 "발행 함수"가 실제로 어떤 이름인지 찾기 위한 디버그 도구
    public sealed class GameEventBusProbe : MonoBehaviour
    {
        [SerializeField] private EventLayerContext eventLayerContext;

        [ContextMenu("Probe/GameEventBus Methods")]
        public void ProbeMethods()
        {
            if (eventLayerContext == null || eventLayerContext.Bus == null)
            {
                Debug.LogError("[GameEventBusProbe] eventLayerContext 또는 Bus가 null입니다. (SceneContext/EventLayer 연결 확인)");
                return;
            }

            var bus = eventLayerContext.Bus;
            var busType = bus.GetType();

            // 1) 모든 인스턴스 메서드 출력(이름/파라미터)
            var methods = busType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            Debug.Log($"[GameEventBusProbe] BusType = {busType.FullName}");
            Debug.Log($"[GameEventBusProbe] Total Methods = {methods.Length}");

            // 2) "이벤트 1개를 받는" 메서드 후보만 추림
            // - 파라미터 1개
            // - 파라미터 타입이 IGameEvent를 받거나, IGameEvent의 상위/하위 타입 가능성
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
                    // 혹시 제네릭/기반타입으로 받아도 후보로 보이게
                    x.Param == typeof(object)
                )
                .ToList();

            if (candidates.Count == 0)
            {
                Debug.LogWarning("[GameEventBusProbe] 파라미터 1개(IGameEvent 계열) 메서드 후보가 없습니다.");
            }
            else
            {
                Debug.Log($"[GameEventBusProbe] 1-parameter candidates = {candidates.Count}");
                foreach (var c in candidates)
                {
                    Debug.Log($"[Candidate] {c.Method.Name}({c.Param.FullName})  access={(c.Method.IsPublic ? "public" : "non-public")}");
                }
            }

            // 3) Register 관련 메서드도 같이 찍어주면 구조 파악에 도움됨
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
