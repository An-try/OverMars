using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New engine", menuName = "Over Mars/Equipment/Engine")]
    public class EngineEquipment : EquipmentItem
    {
        [SerializeField] private float _thrustForce;
        [SerializeField] private float _rotationForce;

        public float ThrustForce => _thrustForce;
        public float RotationForce => _rotationForce;
    }
}
