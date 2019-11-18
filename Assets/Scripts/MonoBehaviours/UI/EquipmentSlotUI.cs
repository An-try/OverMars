using UnityEngine;

namespace OverMars
{
    public class EquipmentSlotUI : SlotUI
    {
        private int _id;
        private Vector2Int _arrayIndexes;
        public bool IsUnderItem = false;
        public bool IsEmptyTile = true;

        public bool IsActiveTile => this._image.enabled;
        public override int Id => _id;
        public override Vector2Int ArrayIndexes => _arrayIndexes;
        public override bool IsEquipmentSlot => true;

        public void SetParameters(int id, Vector2Int arrayIndexes)
        {
            _id = id;
            _arrayIndexes = arrayIndexes;
        }

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

        public void MarkAsUnderItem(Color newColor)
        {
            IsUnderItem = true;
            _image.color = newColor;
        }

        public void MarkAsNotUnderItem()
        {
            IsUnderItem = false;
            _image.color = EquipmentItem ? Color.white : _emptySlotColor;
        }
    }
}
