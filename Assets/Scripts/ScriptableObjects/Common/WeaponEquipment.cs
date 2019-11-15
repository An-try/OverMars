using UnityEngine;

namespace OverMars
{
    public abstract class WeaponEquipment : EquipmentItem
    {
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _range;
        [SerializeField] private float _shootingAngle;

        private const int SECONDS_IN_MINUTE_AMOUNT = 60;
        private const int ONE_SECOND = 1;

        public float ReloadTime => _reloadTime;
        public string RateOfFirePerSecond => (ONE_SECOND / _reloadTime).ToString("0.00") + " shots/sec";
        public string RateOfFirePerMinute => (SECONDS_IN_MINUTE_AMOUNT / _reloadTime).ToString("0.00") + " shots/min";
        public float Range => _range;
        public float ShootingAngle => _shootingAngle;
    }
}
