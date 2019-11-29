using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New reactor-capacitor", menuName = "Over Mars/Equipment/Utility/Power/Reactor-capacitor")]
    public class ReactorCapacitorPower : PowerUtility
    {
        [Header("Reactor Capacitor Power")]
        [SerializeField] private float _energyProduction;
        [SerializeField] private float _energyCapacity;

        public float EnergyProduction => _energyProduction;
        public float EnergyCapacity => _energyCapacity;
    }
}
