// Assets/Scripts/BossRaid/Battle/Loop/BattleLoopExecutor.cs
using UnityEngine;
using BossRaid.Core.Loop;
using BossRaid.Core.Loop.Commands;
using BossRaid.Core.Loop.Occurred;

namespace BossRaid.Battle.Loop
{
    public sealed class BattleLoopExecutor : MonoBehaviour
    {
        private IStep7EventBus _bus;

        private bool _battleRunning;
        private float _battleStartTime;

        public void Initialize(IStep7EventBus bus)
        {
            _bus = bus;

            _bus.Subscribe<EnterBattleCommand>(OnEnterBattleCommand);
            _bus.Subscribe<StartBattleCommand>(OnStartBattleCommand);
            _bus.Subscribe<EndBattleCommand>(OnEndBattleCommand);
            _bus.Subscribe<ExecutePlayerAttackCommand>(OnExecutePlayerAttackCommand);
        }

        private void OnDestroy()
        {
            if (_bus == null) return;

            _bus.Unsubscribe<EnterBattleCommand>(OnEnterBattleCommand);
            _bus.Unsubscribe<StartBattleCommand>(OnStartBattleCommand);
            _bus.Unsubscribe<EndBattleCommand>(OnEndBattleCommand);
            _bus.Unsubscribe<ExecutePlayerAttackCommand>(OnExecutePlayerAttackCommand);
        }

        private void Update()
        {
            if (!_battleRunning) return;

            // Step7 시나리오 #1: Battle 진입 -> 10초 생존 -> 종료
            if (Time.time - _battleStartTime >= 10f)
            {
                _battleRunning = false;
                _bus.Publish(new BattleEndedOccurred(Time.time));
            }
        }

        private void OnEnterBattleCommand(EnterBattleCommand cmd)
        {
            Debug.Log($"[Step7][Exec] EnterBattle actor={cmd.actorId}");
            // 여기서 씬 준비/리셋(전투 성장만 초기화) 등을 수행할 수 있다.
        }

        private void OnStartBattleCommand(StartBattleCommand cmd)
        {
            Debug.Log($"[Step7][Exec] StartBattle actor={cmd.actorId}");
            _battleRunning = true;
            _battleStartTime = Time.time;

            _bus.Publish(new BattleStartedOccurred(_battleStartTime));
        }

        private void OnEndBattleCommand(EndBattleCommand cmd)
        {
            Debug.Log($"[Step7][Exec] EndBattle reason={cmd.reason}");
            _battleRunning = false;
            _bus.Publish(new BattleEndedOccurred(Time.time));
        }

        private void OnExecutePlayerAttackCommand(ExecutePlayerAttackCommand cmd)
        {
            // ✅ 실행만: damage를 적용했다 “사실”만 남긴다.
            Debug.Log($"[Step7][Exec] Attack actor={cmd.actorId} dmg={cmd.damage}");

            int appliedDamage = cmd.damage; // Step7은 임시
            _bus.Publish(new PlayerAttackOccurred(cmd.actorId, appliedDamage));
        }
    }
}