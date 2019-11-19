using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New plasma weapon", menuName = "Over Mars/Equipment/Weapon/Energy/Projectile/Plasma weapon")]
    public class PlasmaProjectile : ProjectileEnergy
    {
        [Header("Plasma Projectile")]
        [SerializeField] [Tooltip("No special parameters for this class")] private string _noParameters = "No parameters";
    }
}
