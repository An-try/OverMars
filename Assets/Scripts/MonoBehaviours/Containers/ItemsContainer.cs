using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public class ItemsContainer : Singleton<ItemsContainer>
    {
        [Header("Ships")]
        [SerializeField] private List<ShipItem> _shipItems;

        [Header("Weapons")]
        [SerializeField] private List<PenetrationWeapon> _penetrationWeapons;
        [SerializeField] private List<RocketWeapon> _rocketWeapons;
        [SerializeField] private List<LaserWeapon> _laserWeapons;

        [Header("Equipment")]
        [SerializeField] private List<ArmorEquipment> _armorEquipments;
        [SerializeField] private List<ShieldEquipment> _shieldEquipments;
        [SerializeField] private List<ReactorEquipment> _reactorEquipments;
        [SerializeField] private List<EngineEquipment> _engineEquipments;

        public List<ShipItem> ShipItems => _shipItems;
        public List<PenetrationWeapon> PenetrationWeapons => _penetrationWeapons;
        public List<RocketWeapon> RocketWeapons => _rocketWeapons;
        public List<LaserWeapon> LaserWeapons => _laserWeapons;
        public List<ArmorEquipment> ArmorEquipments => _armorEquipments;
        public List<ShieldEquipment> ShieldEquipments => _shieldEquipments;
        public List<ReactorEquipment> ReactorEquipments => _reactorEquipments;
        public List<EngineEquipment> EngineEquipments => _engineEquipments;
    }
}
