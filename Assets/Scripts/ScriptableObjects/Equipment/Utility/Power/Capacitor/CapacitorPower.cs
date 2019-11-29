using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New capacitor", menuName = "Over Mars/Equipment/Utility/Power/Capacitor")]
    public class CapacitorPower : PowerUtility
    {
        [Header("Capacitor Power")]
        [SerializeField] private float _energyCapacity;

        public float EnergyCapacity => _energyCapacity;
    }
}
