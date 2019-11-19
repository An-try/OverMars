using UnityEngine;

namespace OverMars
{
    public abstract class RocketWeapon : WeaponEquipment
    {
        [Header("Rocket Weapon")]
        [SerializeField] private float _damagePerRocket;
        [SerializeField] private int _rocketsAmountPerShot;
        [SerializeField] private float _rocketThrustForce;
        [SerializeField] private int _ammunitionСonsumptionPerShot;
        [SerializeField] private float _rocketHitForce;

        public float DamagePerRocket => _damagePerRocket;
        public int RocketsAmountPerShot => _rocketsAmountPerShot;
        public float RocketThrustForce => _rocketThrustForce;
        public int AmmunitionСonsumptionPerShot => _ammunitionСonsumptionPerShot;
        public float RocketHitForce => _rocketHitForce;
        public abstract bool HasRocketAutoGuidanceSystem { get; }
    }
}
