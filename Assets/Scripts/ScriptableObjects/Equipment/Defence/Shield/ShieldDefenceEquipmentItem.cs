using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New shield", menuName = "Over Mars/Equipment/Defence/Shield")]
    public class ShieldDefenceEquipmentItem : DefenceEquipmentItem
    {
#pragma warning disable 0649

        [Header("Shield Defence Equipment Item")]
        [SerializeField] private float _capacity;
        [SerializeField] private float _recoveryPerSecond;
        [SerializeField] private float _radius;

#pragma warning restore 0649

        public float Capacity => _capacity;
        public float RecoveryPerSecond => _recoveryPerSecond;
        public float Radius => _radius;
    }
}
