using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New reactor", menuName = "Over Mars/Equipment/Reactor")]
    public class ReactorEquipment : EquipmentItem
    {
        [SerializeField] private float _energyProduction;
        [SerializeField] private float _explosionDamage;
        [SerializeField] private float _explosionRadius;

        public float EnergyProduction => _energyProduction;
        public float ExplosionDamage => _explosionDamage;
        public float ExplosionRadius => _explosionRadius;
    }
}
