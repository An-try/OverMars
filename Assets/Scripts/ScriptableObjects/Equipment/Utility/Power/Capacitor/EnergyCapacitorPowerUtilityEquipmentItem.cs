using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New energy capacitor", menuName = "Over Mars/Equipment/Utility/Power/Energy capacitor")]
    public class EnergyCapacitorPowerUtilityEquipmentItem : PowerUtilityEquipmentItem
    {
#pragma warning disable 0649

        [Header("Energy Capacitor Power Utility Equipment Item")]
        [SerializeField] private float _energyCapacity;

#pragma warning restore 0649

        public float EnergyCapacity => _energyCapacity;
    }
}
