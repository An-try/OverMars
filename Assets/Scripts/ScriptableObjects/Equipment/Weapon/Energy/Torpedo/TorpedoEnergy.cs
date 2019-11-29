using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New torpedo energy weapon", menuName = "Over Mars/Equipment/Weapon/Energy/Torpedo energy weapon")]
    public class TorpedoEnergy : EnergyWeapon
    {
        [Header("Torpedo Energy")]
        [SerializeField] private float _damagePerTorpedo;
        [SerializeField] private int _torpedosAmountPerShot;
        [SerializeField] private float _torpedoThrustForce;
        [SerializeField] private float _torpedoHitForce;
        [SerializeField] private int _energyСonsumptionPerShot;

        public float DamagePerTorpedo => _damagePerTorpedo;
        public int TorpedosAmountPerShot => _torpedosAmountPerShot;
        public float TorpedoThrustForce => _torpedoThrustForce;
        public float TorpedoHitForce => _torpedoHitForce;
        public int EnergyСonsumptionPerShot => _energyСonsumptionPerShot;
    }
}
