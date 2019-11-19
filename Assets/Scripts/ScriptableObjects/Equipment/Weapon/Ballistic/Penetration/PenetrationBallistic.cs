using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New penetration weapon", menuName = "Over Mars/Equipment/Weapon/Ballistic/Penetration weapon")]
    public class PenetrationBallistic : BallisticWeapon
    {
        [Header("Penetration Ballistic")]
        [SerializeField] [Range(0, 90)] private float _penetrationPercent;

        public float PenetrationPercent => _penetrationPercent;
    }
}
