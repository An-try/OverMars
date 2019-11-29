using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New ammo manufacturer", menuName = "Over Mars/Equipment/Utility/Ammunition/Ammo manufacturer")]
    public class ManufacturerAmmunition : AmmunitionUtility
    {
        [Header("Manufacturer Ammunition")]
        [SerializeField] private float _ammoProductionPerSecond;

        public float AmmoProductionPerSecond => _ammoProductionPerSecond;
    }
}
