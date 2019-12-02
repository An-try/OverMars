using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New energy torpedo weapon", menuName = "Over Mars/Equipment/Weapon/Energy/Energy torpedo weapon")]
    public class TorpedoEnergyWeaponEquipmentItem : EnergyWeaponEquipmentItem
    {
#pragma warning disable 0649

        [Header("Torpedo Energy Weapon Equipment Item")]
        [SerializeField] private float _damagePerTorpedo;
        [SerializeField] private int _torpedosAmountPerShot;
        [SerializeField] private float _torpedoThrustForce;
        [SerializeField] private float _torpedoHitForce;
        [SerializeField] private int _energyСonsumptionPerShot;

#pragma warning restore 0649

        public float DamagePerTorpedo => _damagePerTorpedo;
        public int TorpedosAmountPerShot => _torpedosAmountPerShot;
        public float TorpedoThrustForce => _torpedoThrustForce;
        public float TorpedoHitForce => _torpedoHitForce;
        public int EnergyСonsumptionPerShot => _energyСonsumptionPerShot;
    }
}
