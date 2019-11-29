using UnityEngine;

namespace OverMars
{
    public abstract class ProjectileEnergy : EnergyWeapon
    {
        [Header("Projectile Energy")]
        [SerializeField] private float _damagePerAmmo;
        [SerializeField] private int _ammoAmountPerShot;
        [SerializeField] private float _ammoHitForce;
        [SerializeField] private int _energyСonsumptionPerShot;

        public float DamagePerAmmo => _damagePerAmmo;
        public int AmmoAmountPerShot => _ammoAmountPerShot;
        public float AmmoHitForce => _ammoHitForce;
        public int EnergyСonsumptionPerShot => _energyСonsumptionPerShot;
    }
}
