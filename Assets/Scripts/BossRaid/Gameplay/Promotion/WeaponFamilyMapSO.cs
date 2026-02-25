using System;
using UnityEngine;

namespace BossRaid.Gameplay.Promotion
{
    [CreateAssetMenu(menuName = "BossRaid/Promotion/WeaponFamilyMap", fileName = "WeaponFamilyMapSO")]
    public sealed class WeaponFamilyMapSO : ScriptableObject
    {
        [Serializable]
        public struct Entry
        {
            public string weaponId;
            public string weaponFamilyId;
        }

        public Entry[] entries;
    }
}