using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public class EquipmentSlotUI : SlotUI
    {
        public override bool IsInventorySlot => false;

        public override void SetItem(EquipmentItem equipmentItem)
        {
            EquipmentItem = equipmentItem;
            UpdateSlotUI(equipmentItem);
        }

        private protected override void RemoveItem()
        {
            EquipmentItem = null;
        }
    }
}
