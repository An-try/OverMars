using UnityEngine;

namespace OverMars
{
    public abstract class EquipmentItem : Item
    {
        [SerializeField] private string _type;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private float _energyConsumption;
        [SerializeField] private float _durability;
        [SerializeField] private float _armor;
        [SerializeField] private float _reflection;
        [SerializeField] private float _mass;

        public string Type => _type;
        public Vector2Int Size => _size;
        public float EnergyConsumption => _energyConsumption;
        public float Durability => _durability;
        public float Armor => _armor;
        public float Reflection => _reflection;
        public float Mass => _mass;

        public override bool IsEquipment => true;
    }
}
