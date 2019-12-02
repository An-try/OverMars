using UnityEngine;

namespace OverMars
{
    public abstract class ProjectileEnergyWeaponEquipmentItem : EnergyWeaponEquipmentItem
    {
#pragma warning disable 0649

        [Header("Projectile Energy Weapon Equipment Item")]
        [SerializeField] private float _damagePerAmmo;
        [SerializeField] private int _ammoAmountPerShot;
        [SerializeField] private float _ammoHitForce;
        [SerializeField] private int _energyСonsumptionPerShot;

#pragma warning restore 0649

        public float DamagePerAmmo => _damagePerAmmo;
        public int AmmoAmountPerShot => _ammoAmountPerShot;
        public float AmmoHitForce => _ammoHitForce;
        public int EnergyСonsumptionPerShot => _energyСonsumptionPerShot;
    }
}
