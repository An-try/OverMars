using UnityEngine;

namespace OverMars
{
    public abstract class BallisticWeaponEquipmentItem : WeaponEquipmentItem
    {
#pragma warning disable 0649

        [Header("Ballistic Weapon Equipment Item")]
        [SerializeField] private float _damagePerAmmo;
        [SerializeField] private int _ammoAmountPerShot;
        [SerializeField] private int _ammunitionСonsumptionPerShot;
        [SerializeField] private float _ammoHitForce;

#pragma warning restore 0649

        public float DamagePerAmmo => _damagePerAmmo;
        public int AmmoAmountPerShot => _ammoAmountPerShot;
        public int AmmunitionСonsumptionPerShot => _ammunitionСonsumptionPerShot;
        public float AmmoHitForce => _ammoHitForce;
    }
}
