using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public class ItemSlotUI : SlotUI
    {
        public override bool IsEquipmentSlot => false;

        public override void SetItem(EquipmentItem equipmentItem, List<Vector2Int> itemTilesIndexess)
        {
            EquipmentItem = equipmentItem;
            UpdateSlotUI();
        }

        private protected override void RemoveItem()
        {

        }
    }
}
