using UnityEngine;

namespace OverMars
{
    public abstract class EquipmentItem : Item
    {
#pragma warning disable 0649

        [Header("Equipment Item")]
        [SerializeField] private EquipmentTypes _type;
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private float _mass;
        [SerializeField] private float _energyConsumptionPerSecond;
        [SerializeField] private float _durability;
        [SerializeField] [Range(0, 90)] private int _armor;
        [SerializeField] [Range(0, 90)] private int _reflection;

#pragma warning restore 0649

        public EquipmentTypes Type => _type;
        public int Width => _width;
        public int Height => _height;
        public float Mass => _mass;
        public float EnergyConsumptionPerSecond => _energyConsumptionPerSecond;
        public float Durability => _durability;
        public float Armor => _armor;
        public float Reflection => _reflection;

        public override bool IsEquipment => true;

        public override string GetInfo()
        {
            return base.GetInfo() +
                   "Type: " + _type + "\n" +
                   "Width: " + _width + "\n" +
                   "Height: " + _height + "\n" +
                   "Energy consumption per second: " + _energyConsumptionPerSecond + "\n" +
                   "Durability: " + _durability + "\n" +
                   "Armor: " + _armor + "\n" +
                   "Reflection: " + _reflection + "\n" +
                   "Mass: " + _mass + "\n";
        }
    }
}
