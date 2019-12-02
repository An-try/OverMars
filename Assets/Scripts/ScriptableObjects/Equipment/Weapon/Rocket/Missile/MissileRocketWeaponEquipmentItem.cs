using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New missile weapon", menuName = "Over Mars/Equipment/Weapon/Rocket/Missile weapon")]
    public class MissileRocketWeaponEquipmentItem : RocketWeaponEquipmentItem
    {
#pragma warning disable 0649

        [Header("Missile Rocket Weapon Equipment Item")]
        [SerializeField] private float _missileRotationForce;

#pragma warning restore 0649

        public float MissileRotationForce => _missileRotationForce;
        public override bool HasRocketAutoGuidanceSystem => true;
    }
}
