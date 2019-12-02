using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public class ItemsContainer : Singleton<ItemsContainer>
    {
#pragma warning disable 0649

        [Header("Ships")]
        [SerializeField] private List<ShipItem> _shipItems;

        [Header("Weapon")]
        [SerializeField] private List<WeaponEquipmentItem> _weaponEquipmentItems;

        [Header("Defence")]
        [SerializeField] private List<DefenceEquipmentItem> _defenceEquipmentItems;

        [Header("Utility")]
        [SerializeField] private List<UtilityEquipmentItem> _utilityEquipmentItems;

#pragma warning restore 0649

        public List<EquipmentItem> AllEquipmentItems
        {
            get
            {
                List<EquipmentItem> equipmentItems = new List<EquipmentItem>();

                equipmentItems.AddRange(_weaponEquipmentItems);
                equipmentItems.AddRange(_defenceEquipmentItems);
                equipmentItems.AddRange(_utilityEquipmentItems);

                return equipmentItems;
            }
        }
    }
}
