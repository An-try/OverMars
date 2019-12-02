using UnityEngine;

namespace OverMars
{
    public abstract class PowerUtilityEquipmentItem : UtilityEquipmentItem
    {
#pragma warning disable 0649

        [Header("Power Utility Equipment Item")]
        [SerializeField] private float _explosionDamage;
        [SerializeField] private float _explosionRadius;

#pragma warning restore 0649

        public float ExplosionDamage => _explosionDamage;
        public float ExplosionRadius => _explosionRadius;
    }
}
