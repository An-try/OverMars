using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New laser weapon", menuName = "Over Mars/Equipment/Weapon/Energy/Projectile/Laser weapon")]
    public class LaserProjectile : ProjectileEnergy
    {
        [Header("Laser Projectile")]
        [SerializeField] [Tooltip("No special parameters for this class")] private string _noParameters = "No parameters";
    }
}
