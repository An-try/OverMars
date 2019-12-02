using UnityEngine;

namespace OverMars
{
    public abstract class RocketWeaponEquipmentItem : WeaponEquipmentItem
    {
#pragma warning disable 0649

        [Header("Rocket Weapon Equipment Item")]
        [SerializeField] private float _damagePerRocket;
        [SerializeField] private int _rocketsAmountPerShot;
        [SerializeField] private float _rocketThrustForce;
        [SerializeField] private int _ammunitionСonsumptionPerShot;
        [SerializeField] private float _rocketHitForce;

#pragma warning restore 0649

        public float DamagePerRocket => _damagePerRocket;
        public int RocketsAmountPerShot => _rocketsAmountPerShot;
        public float RocketThrustForce => _rocketThrustForce;
        public int AmmunitionСonsumptionPerShot => _ammunitionСonsumptionPerShot;
        public float RocketHitForce => _rocketHitForce;
        public abstract bool HasRocketAutoGuidanceSystem { get; }
    }
}
