using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New weapon", menuName = "Over Mars/Equipment/Weapon/Rocket weapon")]
    public class RocketWeapon : WeaponEquipment
    {
        [SerializeField] private float _damagePerRocket;
        [SerializeField] private int _rocketsAmountPerShot;

        public float DamagePerRocket => _damagePerRocket;
        public int RocketsAmountPerShot => _rocketsAmountPerShot;
    }
}
