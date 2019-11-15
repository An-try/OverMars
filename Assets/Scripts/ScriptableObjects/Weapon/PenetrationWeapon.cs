using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New weapon", menuName = "Over Mars/Equipment/Weapon/Penetration weapon")]
    public class PenetrationWeapon : WeaponEquipment
    {
        [SerializeField] private float _damagePerAmmo;
        [SerializeField] private int _ammoAmountPerShot;

        public float DamagePerAmmo => _damagePerAmmo;
        public int AmmoAmountPerShot => _ammoAmountPerShot;
    }
}
