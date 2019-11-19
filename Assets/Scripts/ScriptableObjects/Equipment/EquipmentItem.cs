using UnityEngine;

namespace OverMars
{
    public abstract class EquipmentItem : Item
    {
        [Header("Equipment Item")]
        [SerializeField] private EquipmentTypes _type;
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private float _energyConsumption;
        [SerializeField] private float _durability;
        [SerializeField] private float _armor;
        [SerializeField] private float _reflection;
        [SerializeField] private float _mass;

        public EquipmentTypes Type => _type;
        public int Width => _width;
        public int Height => _height;
        public float EnergyConsumption => _energyConsumption;
        public float Durability => _durability;
        public float Armor => _armor;
        public float Reflection => _reflection;
        public float Mass => _mass;

        public override bool IsEquipment => true;
    }
}
