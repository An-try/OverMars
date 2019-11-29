using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New beam weapon", menuName = "Over Mars/Equipment/Weapon/Energy/Beam weapon")]
    public class BeamEnergy : EnergyWeapon
    {
        [Header("Beam Energy")]
        [SerializeField] private float _energyСonsumptionPerSecond;

        public float EnergyСonsumptionPerSecond => _energyСonsumptionPerSecond;
    }
}
