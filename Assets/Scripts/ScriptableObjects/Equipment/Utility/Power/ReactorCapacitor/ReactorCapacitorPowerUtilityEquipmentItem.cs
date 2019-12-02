using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New reactor-capacitor", menuName = "Over Mars/Equipment/Utility/Power/Reactor-capacitor")]
    public class ReactorCapacitorPowerUtilityEquipmentItem : PowerUtilityEquipmentItem
    {
#pragma warning disable 0649

        [Header("Reactor Capacitor Power Utility Equipment Item")]
        [SerializeField] private float _energyProduction;
        [SerializeField] private float _energyCapacity;

#pragma warning restore 0649

        public float EnergyProduction => _energyProduction;
        public float EnergyCapacity => _energyCapacity;
    }
}
