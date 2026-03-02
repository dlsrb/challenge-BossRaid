// Assets/Scripts/BossRaid/Core/Mediators/BattleModifierComposeMediator.cs
using UnityEngine;
using BossRaid.Core.Loop;
using BossRaid.Core.Loop.Commands;
using BossRaid.Core.Loop.Occurred;
using BossRaid.Core.Loop.Requests;

namespace BossRaid.Core.Mediators
{
    public sealed class BattleModifierComposeMediator
    {
        private readonly IStep7EventBus _bus;

        private CoreLoopState _state = CoreLoopState.Lobby;
        private float _battleStartTime = -1f;

        public BattleModifierComposeMediator(IStep7EventBus bus)
        {
            _bus = bus;

            _bus.Subscribe<BattleEnterRequested>(OnBattleEnterRequested);
            _bus.Subscribe<BattleRetryRequested>(OnBattleRetryRequested);
            _bus.Subscribe<PlayerAttackRequested>(OnPlayerAttackRequested);

            _bus.Subscribe<BattleStartedOccurred>(OnBattleStartedOccurred);
            _bus.Subscribe<BattleEndedOccurred>(OnBattleEndedOccurred);
        }

        public void Dispose()
        {
            _bus.Unsubscribe<BattleEnterRequested>(OnBattleEnterRequested);
            _bus.Unsubscribe<BattleRetryRequested>(OnBattleRetryRequested);
            _bus.Unsubscribe<PlayerAttackRequested>(OnPlayerAttackRequested);

            _bus.Unsubscribe<BattleStartedOccurred>(OnBattleStartedOccurred);
            _bus.Unsubscribe<BattleEndedOccurred>(OnBattleEndedOccurred);
        }

        private void OnBattleEnterRequested(BattleEnterRequested req)
        {
            if (_state != CoreLoopState.Lobby && _state != CoreLoopState.Result)
            {
                Debug.Log($"[Step7] EnterRejected state={_state}");
                return;
            }

            TransitionTo(CoreLoopState.Prepare);
            _bus.Publish(new EnterBattleCommand(req.actorId));
            _bus.Publish(new StartBattleCommand(req.actorId));
        }

        private void OnBattleRetryRequested(BattleRetryRequested req)
        {
            if (_state != CoreLoopState.Result)
            {
                Debug.Log($"[Step7] RetryRejected state={_state}");
                return;
            }

            TransitionTo(CoreLoopState.Lobby);
            _bus.Publish(new BattleEnterRequested(req.actorId));
        }

        private void OnPlayerAttackRequested(PlayerAttackRequested req)
        {
            if (_state != CoreLoopState.Battle)
            {
                Debug.Log($"[Step7] AttackIgnored state={_state}");
                return;
            }

            int baseDamage = 10;
            int promotionDamage = baseDamage;
            int talentDamage = promotionDamage;
            int difficultyDamage = talentDamage;
            int finalDamage = ClampDamage(difficultyDamage);

            _bus.Publish(new ExecutePlayerAttackCommand(req.actorId, finalDamage));
        }

        private void OnBattleStartedOccurred(BattleStartedOccurred occ)
        {
            if (_state == CoreLoopState.Prepare)
            {
                TransitionTo(CoreLoopState.Battle);
                _battleStartTime = occ.startTime;
            }
        }

        private void OnBattleEndedOccurred(BattleEndedOccurred occ)
        {
            if (_state != CoreLoopState.Battle) return;

            bool survived10s = (_battleStartTime >= 0f) && (occ.endTime - _battleStartTime >= 10f);
            bool bossDefeated = false;

            var result = bossDefeated ? BattleResultType.Victory : BattleResultType.Defeat;
            string reason = bossDefeated ? "boss_defeated" : survived10s ? "survive_10s_end" : "ended";

            _bus.Publish(new BattleResultOccurred(result, reason));
            TransitionTo(CoreLoopState.Result);
        }

        private void TransitionTo(CoreLoopState next)
        {
            if (_state == next) return;

            var prev = _state;
            _state = next;

            Debug.Log($"[Step7] State {prev} -> {next}");
            _bus.Publish(new BattleStateChangedOccurred(prev, next));
        }

        private static int ClampDamage(int value)
        {
            if (value < 1) return 1;
            if (value > 999) return 999;
            return value;
        }
    }
}
