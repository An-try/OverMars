using UnityEngine;

namespace OverMars
{
    public abstract class BallisticWeapon : WeaponEquipment
    {
        [Header("Ballistic Weapon")]
        [SerializeField] private float _damagePerAmmo;
        [SerializeField] private int _ammoAmountPerShot;
        [SerializeField] private int _ammunitionСonsumptionPerShot;
        [SerializeField] private float _ammoHitForce;

        public float DamagePerAmmo => _damagePerAmmo;
        public int AmmoAmountPerShot => _ammoAmountPerShot;
        public int AmmunitionСonsumptionPerShot => _ammunitionСonsumptionPerShot;
        public float AmmoHitForce => _ammoHitForce;
    }
}
