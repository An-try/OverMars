using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New torpedo weapon", menuName = "Over Mars/Equipment/Weapon/Rocket/Torpedo weapon")]
    public class TorpedoRocketWeaponEquipmentItem : RocketWeaponEquipmentItem
    {
        public override bool HasRocketAutoGuidanceSystem => false;
    }
}
