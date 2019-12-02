using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New beam weapon", menuName = "Over Mars/Equipment/Weapon/Energy/Beam weapon")]
    public class BeamEnergyWeaponEquipmentItem : EnergyWeaponEquipmentItem
    {
#pragma warning disable 0649

        [Header("Beam Energy Weapon Equipment Item")]
        [SerializeField] private float _energyСonsumptionPerSecond;

#pragma warning restore 0649

        public float EnergyСonsumptionPerSecond => _energyСonsumptionPerSecond;
    }
}
