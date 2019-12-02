using UnityEngine;

namespace OverMars
{
    public abstract class WeaponEquipmentItem : EquipmentItem
    {
#pragma warning disable 0649

        [Header("Weapon Equipment Item")]
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _range;
        [SerializeField] private float _shootingAngle;
        [SerializeField] private float _turnRate;
        [SerializeField] [Range(-100, 100)] private float _percentOfExtraDamageToArmor;
        [SerializeField] [Range(-100, 100)] private int _percentOfExtraDamageToShields;

#pragma warning restore 0649

        private const int SECONDS_IN_MINUTE_AMOUNT = 60;
        private const int ONE_SECOND = 1;

        public float ReloadTime => _reloadTime;
        public string RateOfFirePerSecond => (ONE_SECOND / _reloadTime).ToString("0.00") + " shots/sec";
        public string RateOfFirePerMinute => (SECONDS_IN_MINUTE_AMOUNT / _reloadTime).ToString("0.00") + " shots/min";
        public float Range => _range;
        public float ShootingAngle => _shootingAngle;
        public float TurnRate => _turnRate;
        public float PercentOfExtraDamageToArmor => _percentOfExtraDamageToArmor;
        public int PercentOfExtraDamageToShields => _percentOfExtraDamageToShields;
    }
}
