using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New ammo storage", menuName = "Over Mars/Equipment/Utility/Ammunition/Storage")]
    public class StorageAmmunition : AmmunitionUtility
    {
        [Header("Storage Ammunition")]
        [SerializeField] private float _ammoStorageCapacity;

        public float AmmoStorageCapacity => _ammoStorageCapacity;
    }
}
