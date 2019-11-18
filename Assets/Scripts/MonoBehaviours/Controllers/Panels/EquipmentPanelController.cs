using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OverMars
{
    public class EquipmentPanelController : PanelCommon<EquipmentPanelController>
    {
        [SerializeField] private Transform _shipTilesContainerUI;
        [SerializeField] private GameObject _shipTileUIPrefab;

        private static EquipmentSlotUI[,] _equipmentGrid;

        private static Color _itemSuitableColor = Color.green;
        private static Color _itemUnsuitableColor = Color.red;

        private static List<Vector2Int> _equipmentSlotsUnderDragAndDropObjectArrayIndexes = new List<Vector2Int>();

        private void Start()
        {
            RebuildTiles();
        }

        public static void SetSlotsUnderItemAreNotEmpty()
        {
            for (int i = 0; i < _equipmentGrid.GetLength(0); i++)
            {
                for (int j = 0; j < _equipmentGrid.GetLength(1); j++)
                {
                    if (_equipmentGrid[i, j].IsUnderItem)
                    {
                        _equipmentGrid[i, j].IsEmptyTile = false;
                    }
                }
            }
        }

        #region Building ship tiles UI

        private void RebuildTiles()
        {
            DestroyTiles();

            ShipItem shipItem = PlayerController.Instance.Ship.ShipItem;
            string cleanTilesCode = shipItem.CleanTilesCode;
            Vector2Int size = shipItem.Size;
            Vector2Int starterPoint = new Vector2Int((int)_shipTilesContainerUI.localPosition.x - size.x / 2, (int)_shipTilesContainerUI.localPosition.y + size.y / 2);

            _equipmentGrid = new EquipmentSlotUI[size.x, size.y];
            int tileIndex = size.x * size.y - 1;

            for (int i = size.x - 1; i >= 0; i--)
            {
                for (int j = size.y - 1; j >= 0; j--)
                {
                    int imageHeight = (int)_shipTileUIPrefab.GetComponent<RectTransform>().sizeDelta.y;
                    int imageHalfSizeY = imageHeight / 2;
                    Vector3 newTilePosition = new Vector3((starterPoint.x + j + 1) * imageHeight - imageHalfSizeY, (starterPoint.y - i + 1) * imageHeight + imageHalfSizeY, 0);

                    EquipmentSlotUI equipmentSlotUI = Instantiate(_shipTileUIPrefab, _shipTilesContainerUI).GetComponent<EquipmentSlotUI>();
                    equipmentSlotUI.transform.localPosition = newTilePosition;
                    equipmentSlotUI.SetParameters(tileIndex, new Vector2Int(i, j));

                    int tileCode = int.Parse(cleanTilesCode[tileIndex].ToString());
                    if (tileCode == 0)
                    {
                        equipmentSlotUI.DeactivateTile();
                    }
                    else
                    {
                        equipmentSlotUI.ActivateTile((TileTypes)tileCode);
                    }

                    _equipmentGrid[i, j] = equipmentSlotUI;
                    tileIndex--;
                }
            }
        }

        private void DestroyTiles()
        {
            for (; _shipTilesContainerUI.childCount > 0;)
            {
                Destroy(_shipTilesContainerUI.GetChild(0));
            }
        }

        #endregion

        #region Check drag_and_drop_item for suitability

        public static void CheckDragAndDropItemForSuitability(List<Vector2Int> equipmentSlotsUnderDragAndDropObjectArrayIndexes, Vector2Int itemSize)
        {
            _equipmentSlotsUnderDragAndDropObjectArrayIndexes = equipmentSlotsUnderDragAndDropObjectArrayIndexes;
            bool suitable = true;
            foreach (Vector2Int arrayIndex in _equipmentSlotsUnderDragAndDropObjectArrayIndexes)
            {
                if (arrayIndex.x < 0 || arrayIndex.y < 0 ||
                    arrayIndex.x >= _equipmentGrid.GetLength(0) || arrayIndex.y >= _equipmentGrid.GetLength(1) ||
                    !_equipmentGrid[arrayIndex.x, arrayIndex.y].IsActiveTile)
                {
                    suitable = false;
                    break;
                }
            }

            MarkTilesAsNotUnderItem();
            MarkTilesAsUnderItem(suitable, itemSize);
        }

        public static void MarkTilesAsNotUnderItem()
        {
            for (int i = 0; i < _equipmentGrid.GetLength(0); i++)
            {
                for (int j = 0; j < _equipmentGrid.GetLength(1); j++)
                {
                    _equipmentGrid[i, j].MarkAsNotUnderItem();
                }
            }
        }

        private static void MarkTilesAsUnderItem(bool suitable, Vector2Int itemSize)
        {
            int indexOffsetX = 0;
            int indexOffsetY = 0;

            if (itemSize.x > 1)
            {
                indexOffsetX = 1;
            }
            if (itemSize.y > 1)
            {
                indexOffsetY = 1;
            }

            foreach (Vector2Int arrayIndex in _equipmentSlotsUnderDragAndDropObjectArrayIndexes)
            {
                for (int i = arrayIndex.x; i < itemSize.x + arrayIndex.x - indexOffsetX; i++)
                {
                    for (int j = arrayIndex.y; j < itemSize.y + arrayIndex.y - indexOffsetY; j++)
                    {
                        Color newColor = suitable ? _itemSuitableColor : _itemUnsuitableColor;
                        try
                        {
                            _equipmentGrid[i, j].MarkAsUnderItem(newColor);
                        }
                        catch { }
                    }
                }
            }
        }

        #endregion
    }
}
