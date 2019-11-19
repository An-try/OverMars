using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New impact weapon", menuName = "Over Mars/Equipment/Weapon/Ballistic/Impact weapon")]
    public class ImpactBallistic : BallisticWeapon
    {
        [Header("Impact Ballistic")]
        [SerializeField] [Tooltip("No special parameters for this class")] private string _noParameters = "No parameters";
    }
}
