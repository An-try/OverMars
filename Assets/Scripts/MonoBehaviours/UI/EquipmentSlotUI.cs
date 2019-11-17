using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public class EquipmentSlotUI : SlotUI
    {
        public override bool IsEquipmentSlot => true;

        public void ActivateTile(TileTypes tileType)
        {
            switch (tileType)
            {
                case TileTypes.Basic:
                    _image.color = Color.white;
                    break;
                case TileTypes.Engine:
                    _image.color = Color.blue;
                    break;
                default:
                    _image.color = Color.white;
                    break;
            }

            _image.enabled = true;
        }

        public void DeactivateTile()
        {
            _image.enabled = false;
        }

        public override void SetItem(EquipmentItem equipmentItem)
        {
            EquipmentItem = equipmentItem;
            UpdateSlotUI();
        }

        private protected override void RemoveItem()
        {
            EquipmentItem = null;
            UpdateSlotUI();
        }
    }
}
