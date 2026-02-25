using System.Collections.Generic;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Requested;
using BossRaid.Core.Events.Occurred;
using BossRaid.Gameplay.Promotion;

namespace BossRaid.Core.Mediators
{
    /// <summary>
    /// Promotion 후보 제공/선택/잠금 판단의 단일 지점.
    /// - UI는 표시만, 유효성 판단은 여기서만.
    /// - 전투 시작 시 잠금 (전투 중 변경 불가).
    /// </summary>
    public sealed class PromotionMediator :
        IGameEventHandler<PromotionCandidatesRequested>,
        IGameEventHandler<PromotionSelectRequested>,
        IGameEventHandler<PromotionLockRequested>
    {
        private readonly IGameEventBus _bus;
        private readonly string _sourceId;

        private readonly Dictionary<string, PromotionDefinitionSO> _promotionById;
        private readonly Dictionary<string, List<string>> _candidatesByFamily;

        private bool _locked;
        private string _selectedPromotionId;

        public PromotionMediator(
            IGameEventBus bus,
            string sourceId,
            IEnumerable<PromotionDefinitionSO> promotionDefinitions)
        {
            _bus = bus;
            _sourceId = sourceId;

            _promotionById = new Dictionary<string, PromotionDefinitionSO>();
            _candidatesByFamily = new Dictionary<string, List<string>>();

            if (promotionDefinitions != null)
            {
                foreach (var p in promotionDefinitions)
                {
                    if (p == null) continue;
                    if (string.IsNullOrEmpty(p.promotionId)) continue;
                    if (string.IsNullOrEmpty(p.weaponFamilyId)) continue;

                    _promotionById[p.promotionId] = p;

                    if (!_candidatesByFamily.TryGetValue(p.weaponFamilyId, out var list))
                    {
                        list = new List<string>();
                        _candidatesByFamily[p.weaponFamilyId] = list;
                    }
                    if (!list.Contains(p.promotionId))
                        list.Add(p.promotionId);
                }
            }

            _locked = false;
            _selectedPromotionId = null;
        }

        public bool TryGetSelected(out PromotionDefinitionSO promotion)
        {
            promotion = null;
            if (string.IsNullOrEmpty(_selectedPromotionId)) return false;
            return _promotionById.TryGetValue(_selectedPromotionId, out promotion);
        }

        public void Handle(PromotionCandidatesRequested e)
        {
            if (e == null) return;

            if (!_candidatesByFamily.TryGetValue(e.WeaponFamilyId, out var list))
            {
                _bus.Publish(new PromotionCandidatesProvidedOccurred(_sourceId, e.WeaponFamilyId, new string[0]));
                return;
            }

            // Step6: 해금/조건 시스템이 아직 없으므로 "고정 후보 목록" 그대로 제공.
            // (추후 Step7+에서 해금 조건이 생기면 여기서 필터링)
            _bus.Publish(new PromotionCandidatesProvidedOccurred(_sourceId, e.WeaponFamilyId, list.ToArray()));
        }

        public void Handle(PromotionSelectRequested e)
        {
            if (e == null) return;
            if (_locked) return;

            if (string.IsNullOrEmpty(e.PromotionId)) return;
            if (!_promotionById.ContainsKey(e.PromotionId)) return;

            _selectedPromotionId = e.PromotionId;
            _bus.Publish(new PromotionSelectedOccurred(_sourceId, _selectedPromotionId));
        }

        public void Handle(PromotionLockRequested e)
        {
            if (e == null) return;

            if (_locked) return;
            _locked = true;
            _bus.Publish(new PromotionLockedOccurred(_sourceId));
        }
    }
}
