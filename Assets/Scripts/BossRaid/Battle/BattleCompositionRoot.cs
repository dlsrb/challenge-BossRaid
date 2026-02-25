using UnityEngine;
using BossRaid.Core.Events;
using BossRaid.Core.Events.Command;
using BossRaid.Core.Events.Requested;
using BossRaid.Core.Mediators;
using BossRaid.Core.Mediators.PromotionEffects;
using BossRaid.Core.Mediators.PromotionEffects.Rules;
using BossRaid.Gameplay.Boss;
using BossRaid.Gameplay.Player;
using BossRaid.Gameplay.Promotion;

public sealed class BattleCompositionRoot : MonoBehaviour
{
    [SerializeField] private EventLayerContext eventLayerContext;
    [SerializeField] private BossActorExecutor boss;
    [SerializeField] private PlayerWeaponExecutor playerWeaponExecutor;

    [Header("Catalogs")]
    [SerializeField] private PromotionDefinitionSO[] promotionDefinitions;

    [Header("Weapon Family Map (weaponId -> weaponFamilyId)")]
    [SerializeField] private WeaponFamilyMapSO weaponFamilyMap;

    private BattleMediator _battleMediator;
    private PromotionMediator _promotionMediator;
    private WeaponPromotionComposeMediator _weaponPromotionMediator;

    private GameEventBus _bus;
    private bool _wired;

    private void Awake()
    {
        if (eventLayerContext == null)
        {
            Debug.LogError("[BattleCompositionRoot] eventLayerContext is NULL (check Inspector wiring)");
            return;
        }

        if (boss == null)
        {
            Debug.LogError("[BattleCompositionRoot] boss is NULL (check Inspector wiring)");
            return;
        }

        if (playerWeaponExecutor == null)
        {
            Debug.LogError("[BattleCompositionRoot] playerWeaponExecutor is NULL (attach and wire in the Inspector)");
            return;
        }

        eventLayerContext.EnsureInitialized();
        if (eventLayerContext.Bus == null)
        {
            Debug.LogError("[BattleCompositionRoot] eventLayerContext.Bus is NULL (initialization order)");
            return;
        }

        _bus = eventLayerContext.Bus;

        _battleMediator = new BattleMediator(_bus, "Mediator");
        _bus.Register<ActorActionRequested>(_battleMediator);
        _bus.Register<ActorCommandIssued>(boss);

        _promotionMediator = new PromotionMediator(_bus, "PromotionMediator", promotionDefinitions);
        _bus.Register<PromotionCandidatesRequested>(_promotionMediator);
        _bus.Register<PromotionSelectRequested>(_promotionMediator);
        _bus.Register<PromotionLockRequested>(_promotionMediator);

        var rules = new PromotionEffectRuleRegistry(new IPromotionEffectRule[]
        {
            new DamageMulFromModifierRule(),
            new CooldownMulFromModifierRule(),
        });

        var weaponFamilyResolver = new WeaponFamilyResolver(weaponFamilyMap);

        _weaponPromotionMediator = new WeaponPromotionComposeMediator(
            _bus,
            "WeaponPromotionComposeMediator",
            _promotionMediator,
            rules,
            weaponFamilyResolver
        );
        _bus.Register<WeaponEquipRequested>(_weaponPromotionMediator);
        _bus.Register<WeaponUseRequested>(_weaponPromotionMediator);

        _bus.Register<WeaponAttackCommandIssued>(playerWeaponExecutor);

        boss.Init(_bus);
        playerWeaponExecutor.Init(_bus);

        _wired = true;
    }

    private void OnDestroy()
    {
        if (!_wired || _bus == null) return;

        _bus.Unregister<ActorActionRequested>(_battleMediator);
        _bus.Unregister<ActorCommandIssued>(boss);

        _bus.Unregister<PromotionCandidatesRequested>(_promotionMediator);
        _bus.Unregister<PromotionSelectRequested>(_promotionMediator);
        _bus.Unregister<PromotionLockRequested>(_promotionMediator);

        _bus.Unregister<WeaponEquipRequested>(_weaponPromotionMediator);
        _bus.Unregister<WeaponUseRequested>(_weaponPromotionMediator);
        _bus.Unregister<WeaponAttackCommandIssued>(playerWeaponExecutor);
    }
}
