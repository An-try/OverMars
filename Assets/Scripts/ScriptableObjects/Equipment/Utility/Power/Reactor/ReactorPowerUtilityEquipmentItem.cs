using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New reactor", menuName = "Over Mars/Equipment/Utility/Power/Reactor")]
    public class ReactorPowerUtilityEquipmentItem : PowerUtilityEquipmentItem
    {
#pragma warning disable 0649

        [Header("Reactor Power Utility Equipment Item")]
        [SerializeField] private float _energyProduction;

#pragma warning restore 0649

        public float EnergyProduction => _energyProduction;
    }
}
