using UnityEngine;

namespace OverMars
{
    public abstract class EnergyWeapon : WeaponEquipment
    {
        [Header("Energy Weapon")]
        [SerializeField] private int _energyСonsumptionPerShot;

        public int EnergyСonsumptionPerShot => _energyСonsumptionPerShot;
    }
}
