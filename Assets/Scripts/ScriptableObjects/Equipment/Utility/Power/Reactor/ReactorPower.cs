using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New reactor", menuName = "Over Mars/Equipment/Utility/Power/Reactor")]
    public class ReactorPower : PowerUtility
    {
        [Header("Reactor Power")]
        [SerializeField] private float _energyProduction;

        public float EnergyProduction => _energyProduction;
    }
}
