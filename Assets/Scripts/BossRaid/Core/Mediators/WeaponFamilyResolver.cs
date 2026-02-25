using System.Collections.Generic;
using BossRaid.Gameplay.Promotion;


namespace BossRaid.Core.Mediators
{
    /// <summary>
    /// WeaponDefinition 수정 없이 weaponId -> weaponFamilyId를 제공하는 해석기.
    /// </summary>
    public sealed class WeaponFamilyResolver
    {
        private readonly Dictionary<string, string> _byWeaponId = new Dictionary<string, string>();
      
        public WeaponFamilyResolver(WeaponFamilyMapSO map)
        {
            if (map == null || map.entries == null) return;

            foreach (var e in map.entries)
            {
                if (string.IsNullOrEmpty(e.weaponId)) continue;
                if (string.IsNullOrEmpty(e.weaponFamilyId)) continue;
                _byWeaponId[e.weaponId] = e.weaponFamilyId;
            }
        }

       public bool TryGetFamilyId(string weaponId, out string familyId)
{
    UnityEngine.Debug.Log($"[WeaponFamilyResolver] loaded entries={_byWeaponId.Count}");
    var key = weaponId?.Trim();
    var ok = _byWeaponId.TryGetValue(key, out familyId);

    if (!ok)
        UnityEngine.Debug.LogWarning(
            $"[WeaponFamilyResolver] MISS weaponId='{weaponId}' (trim='{key}')"
        );

    return ok;
}
    }
}