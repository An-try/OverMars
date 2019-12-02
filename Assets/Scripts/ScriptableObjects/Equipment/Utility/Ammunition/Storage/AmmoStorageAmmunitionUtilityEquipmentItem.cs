using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New ammo storage", menuName = "Over Mars/Equipment/Utility/Ammunition/Ammo storage")]
    public class AmmoStorageAmmunitionUtilityEquipmentItem : AmmunitionUtilityEquipmentItem
    {
#pragma warning disable 0649

        [Header("Ammo Storage Ammunition Utility Equipment Item")]
        [SerializeField] private float _ammoStorageCapacity;

#pragma warning restore 0649

        public float AmmoStorageCapacity => _ammoStorageCapacity;
    }
}
