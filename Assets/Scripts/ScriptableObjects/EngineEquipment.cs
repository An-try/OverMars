using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New engine", menuName = "Over Mars/Equipment/Engine")]
    public class EngineEquipment : EquipmentItem
    {
        [SerializeField] private float _thrust;
        [SerializeField] private float _rotationThrust;

        public float Thrust => _thrust;
        public float КotationThrust => _rotationThrust;
    }
}
