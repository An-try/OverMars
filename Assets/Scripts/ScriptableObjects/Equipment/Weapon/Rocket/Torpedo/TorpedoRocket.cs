using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New torpedo weapon", menuName = "Over Mars/Equipment/Weapon/Rocket/Torpedo weapon")]
    public class TorpedoRocket : RocketWeapon
    {
        public override bool HasRocketAutoGuidanceSystem => false;
    }
}
