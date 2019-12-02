using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New engine", menuName = "Over Mars/Equipment/Utility/Engine")]
    public class EngineUtilityEquipmentItem : UtilityEquipmentItem
    {
#pragma warning disable 0649

        [Header("Engine Utility Equipment Item")]
        [SerializeField] private float _maxThrustForce;
        [SerializeField] private float _maxRotationForce;
        [SerializeField] private float _energyConsumptionPerOneThrustForce;
        [SerializeField] private float _energyConsumptionPerOneRotationForce;

#pragma warning restore 0649

        public float MaxThrustForce => _maxThrustForce;
        public float MaxRotationForce => _maxRotationForce;
        public float EnergyConsumptionPerOneThrustForce => _energyConsumptionPerOneThrustForce;
        public float EnergyConsumptionPerOneRotationForce => _energyConsumptionPerOneRotationForce;
    }
}
