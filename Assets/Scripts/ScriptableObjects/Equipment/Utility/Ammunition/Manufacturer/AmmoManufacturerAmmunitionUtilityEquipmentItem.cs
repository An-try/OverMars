using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New ammo manufacturer", menuName = "Over Mars/Equipment/Utility/Ammunition/Ammo manufacturer")]
    public class AmmoManufacturerAmmunitionUtilityEquipmentItem : AmmunitionUtilityEquipmentItem
    {
#pragma warning disable 0649

        [Header("Ammo Manufacturer Ammunition Equipment Item")]
        [SerializeField] private float _ammoProductionPerSecond;

#pragma warning restore 0649

        public float AmmoProductionPerSecond => _ammoProductionPerSecond;
    }
}
