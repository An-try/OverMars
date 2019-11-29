using UnityEngine;

namespace OverMars
{
    public abstract class PowerUtility : UtilityEquipment
    {
        [Header("Power Utility")]
        [SerializeField] private float _explosionDamage;
        [SerializeField] private float _explosionRadius;

        public float ExplosionDamage => _explosionDamage;
        public float ExplosionRadius => _explosionRadius;
    }
}
