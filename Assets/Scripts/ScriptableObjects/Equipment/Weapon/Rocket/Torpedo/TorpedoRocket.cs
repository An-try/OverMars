using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New torpedo weapon", menuName = "Over Mars/Equipment/Weapon/Rocket/Torpedo weapon")]
    public class TorpedoRocket : RocketWeapon
    {
        [Header("Torpedo Rocket")]
        [SerializeField] [Tooltip("No special parameters for this class")] private string _noParameters = "No parameters";

        public override bool HasRocketAutoGuidanceSystem => false;
    }
}
