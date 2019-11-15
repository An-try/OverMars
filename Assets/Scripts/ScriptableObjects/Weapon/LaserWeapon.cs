using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New weapon", menuName = "Over Mars/Equipment/Weapon/Laser weapon")]
    public class LaserWeapon : WeaponEquipment
    {
        [SerializeField] private float _shotDuration;

        public float ShotDuration => _shotDuration;
    }
}
