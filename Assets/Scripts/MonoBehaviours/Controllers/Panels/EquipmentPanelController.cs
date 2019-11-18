using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OverMars
{
    public class EquipmentPanelController : PanelCommon<EquipmentPanelController>
    {
        [SerializeField] private Transform _shipTilesContainerUI;
        [SerializeField] private GameObject _shipTileUIPrefab;

        public static EquipmentSlotUI[,] EquipmentTilesGrid;
        public static List<Vector2Int> EquipmentSlotsUnderDragAndDropObjectArrayIndexes = new List<Vector2Int>();
        public static bool IsItemInDragAndDropSuitable = false;

        private static Color _itemSuitableColor = Color.green;
        private static Color _itemUnsuitableColor = Color.red;

        private void Start()
        {
            RebuildTiles();
        }

        public static void SetSlotsUnderItemAreNotEmpty()
        {
            for (int i = 0; i < EquipmentTilesGrid.GetLength(0); i++)
            {
                for (int j = 0; j < EquipmentTilesGrid.GetLength(1); j++)
                {
                    if (EquipmentTilesGrid[i, j].IsUnderItem)
                    {
                        EquipmentTilesGrid[i, j].IsEmptyTile = false;
                    }
                }
            }
        }

        #region Building ship tiles UI

        private void RebuildTiles()
        {
            DestroyTiles();
            _shipTilesContainerUI.localPosition = Vector3.zero;

            ShipItem shipItem = PlayerController.Instance.Ship.ShipItem;
            string cleanTilesCode = shipItem.CleanTilesCode;
            int shipWidth = shipItem.Width;
            int shipHeight = shipItem.Height;

            EquipmentTilesGrid = new EquipmentSlotUI[shipHeight, shipWidth];
            Vector2Int imageSize = new Vector2Int((int)_shipTileUIPrefab.GetComponent<RectTransform>().sizeDelta.x, (int)_shipTileUIPrefab.GetComponent<RectTransform>().sizeDelta.y);
            int imageHalfWidth = imageSize.x / 2;
            int imageHalfHeight = imageSize.y / 2;

            Vector2Int starterPoint = new Vector2Int((int)_shipTilesContainerUI.localPosition.x - shipWidth / 2,
                                                     (int)_shipTilesContainerUI.localPosition.y + shipHeight / 2);

            int tileIndex = shipWidth * shipHeight - 1;

            for (int i = shipHeight - 1; i >= 0; i--)
            {
                for (int j = shipWidth - 1; j >= 0; j--)
                {
                    Vector3 newTilePosition = new Vector3((starterPoint.x + j) * imageSize.y - imageHalfHeight, (starterPoint.y - i) * imageSize.y + imageHalfHeight, 0);

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

                    EquipmentTilesGrid[i, j] = equipmentSlotUI;
                    tileIndex--;
                }
            }

            if (shipWidth % 2 == 0)
            {
                _shipTilesContainerUI.localPosition += new Vector3(imageHalfWidth, 0, 0);
            }
            if (shipHeight % 2 == 0)
            {
                _shipTilesContainerUI.localPosition -= new Vector3(0, imageHalfHeight, 0);
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
            EquipmentSlotsUnderDragAndDropObjectArrayIndexes = equipmentSlotsUnderDragAndDropObjectArrayIndexes;
            IsItemInDragAndDropSuitable = true;
            foreach (Vector2Int arrayIndex in EquipmentSlotsUnderDragAndDropObjectArrayIndexes)
            {
                if (arrayIndex.x < 0 ||
                    arrayIndex.y < 0 ||
                    arrayIndex.x >= EquipmentTilesGrid.GetLength(0) ||
                    arrayIndex.y >= EquipmentTilesGrid.GetLength(1) ||
                    !EquipmentTilesGrid[arrayIndex.x, arrayIndex.y].IsActiveTile ||
                    !EquipmentTilesGrid[arrayIndex.x, arrayIndex.y].IsEmptyTile)
                {
                    IsItemInDragAndDropSuitable = false;
                    break;
                }
            }

            MarkTilesAsNotUnderItem();
            MarkTilesAsUnderItem(IsItemInDragAndDropSuitable, itemSize);
        }

        public static void MarkTilesAsNotUnderItem()
        {
            for (int i = 0; i < EquipmentTilesGrid.GetLength(0); i++)
            {
                for (int j = 0; j < EquipmentTilesGrid.GetLength(1); j++)
                {
                    EquipmentTilesGrid[i, j].MarkAsNotUnderItem();
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

            foreach (Vector2Int arrayIndex in EquipmentSlotsUnderDragAndDropObjectArrayIndexes)
            {
                for (int i = arrayIndex.x; i < itemSize.x + arrayIndex.x - indexOffsetX; i++)
                {
                    for (int j = arrayIndex.y; j < itemSize.y + arrayIndex.y - indexOffsetY; j++)
                    {
                        Color newColor = suitable ? _itemSuitableColor : _itemUnsuitableColor;
                        try
                        {
                            EquipmentTilesGrid[i, j].MarkAsUnderItem(newColor);
                        }
                        catch { }
                    }
                }
            }
        }

        #endregion
    }
}
