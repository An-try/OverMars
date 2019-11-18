using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public class EquipmentSlotUI : SlotUI
    {
        public bool IsUnderItem = false;
        public bool IsEmptyTile = true;

        private int _id;
        private Vector2Int _arrayIndexes;
        private List<Vector2Int> _itemTilesIndexes = new List<Vector2Int>();

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

        public override void SetItem(EquipmentItem equipmentItem, List<Vector2Int> itemTilesIndexes)
        {
            EquipmentItem = equipmentItem;
            _itemTilesIndexes = itemTilesIndexes;
            UpdateSlotUI();
        }

        private protected override void RemoveItem()
        {
            EquipmentItem = null;
            foreach (Vector2Int tileIndex in _itemTilesIndexes)
            {
                EquipmentPanelController.EquipmentTilesGrid[tileIndex.x, tileIndex.y].IsEmptyTile = true;
            }
            _itemTilesIndexes = null;
            _itemTilesIndexes = new List<Vector2Int>();
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
