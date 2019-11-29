using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New shield", menuName = "Over Mars/Equipment/Defence/Shield")]
    public class ShieldDefence : DefenceEquipment
    {
        [Header("ShieldDefence")]
        [SerializeField] private float _capacity;
        [SerializeField] private float _recoveryPerSecond;
        [SerializeField] private float _radius;

        public float Capacity => _capacity;
        public float RecoveryPerSecond => _recoveryPerSecond;
        public float Radius => _radius;
    }
}
