using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New engine", menuName = "Over Mars/Equipment/Engine")]
    public class EngineEquipment : EquipmentItem
    {
#pragma warning disable 0649

        [SerializeField] private float _thrustForce;
        [SerializeField] private float _rotationForce;

#pragma warning restore 0649

        public float ThrustForce => _thrustForce;
        public float RotationForce => _rotationForce;
    }
}
