using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New missile weapon", menuName = "Over Mars/Equipment/Weapon/Rocket/Missile weapon")]
    public class MissileRocket : RocketWeapon
    {
        [Header("Missile Rocket")]
        [SerializeField] private float _missileRotationForce;

        public float MissileRotationForce => _missileRotationForce;
        public override bool HasRocketAutoGuidanceSystem => true;
    }
}
