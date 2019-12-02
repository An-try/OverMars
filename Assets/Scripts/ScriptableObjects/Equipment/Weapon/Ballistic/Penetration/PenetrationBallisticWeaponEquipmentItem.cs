using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New penetration weapon", menuName = "Over Mars/Equipment/Weapon/Ballistic/Penetration weapon")]
    public class PenetrationBallisticWeaponEquipmentItem : BallisticWeaponEquipmentItem
    {
#pragma warning disable 0649

        [Header("Penetration Ballistic Weapon Item")]
        [SerializeField] [Range(0, 90)] private float _penetrationPercent;

#pragma warning restore 0649

        public float PenetrationPercent => _penetrationPercent;
    }
}
