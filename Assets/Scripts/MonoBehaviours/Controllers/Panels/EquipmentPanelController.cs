using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OverMars
{
    public class EquipmentPanelController : PanelCommon<EquipmentPanelController>
    {
        [SerializeField] private Transform _shipTilesContainerUI;
        [SerializeField] private GameObject _shipTileUIPrefab;

        private GridLayoutGroup _tilesContainerGrid;

        private static EquipmentSlotUI[,] _equipmentGrid;

        private static Color _itemSuitableColor = Color.green;
        private static Color _itemUnsuitableColor = Color.red;

        private protected override void Awake()
        {
            base.Awake();
            _tilesContainerGrid = _shipTilesContainerUI.GetComponent<GridLayoutGroup>();
        }

        private void Start()
        {
            RebuildTiles();
        }

        #region Building ship tiles UI

        private void RebuildTiles()
        {
            DestroyTiles();

            ShipItem shipItem = PlayerController.Instance.Ship.ShipItem;
            string cleanTilesCode = shipItem.CleanTilesCode;
            Vector2Int size = shipItem.Size;
            _tilesContainerGrid.constraintCount = size.y;

            _equipmentGrid = new EquipmentSlotUI[size.x, size.y];
            int tileIndex = 0;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    EquipmentSlotUI equipmentSlotUI = Instantiate(_shipTileUIPrefab, _shipTilesContainerUI).GetComponent<EquipmentSlotUI>();
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
                    tileIndex++;
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
            bool suitable = true;
            foreach (Vector2Int arrayIndex in equipmentSlotsUnderDragAndDropObjectArrayIndexes)
            {
                if (arrayIndex.x < 0 || arrayIndex.y < 0 ||
                    arrayIndex.x >= _equipmentGrid.GetLength(0) || arrayIndex.y >= _equipmentGrid.GetLength(1) ||
                    !_equipmentGrid[arrayIndex.x, arrayIndex.y].IsActiveTile)
                {
                    suitable = false;
                    break;
                }
            }

            SetTilesDefaultColor();
            SetEquipmentTilesColor(suitable, equipmentSlotsUnderDragAndDropObjectArrayIndexes, itemSize);
        }

        public static void SetTilesDefaultColor()
        {
            for (int i = 0; i < _equipmentGrid.GetLength(0); i++)
            {
                for (int j = 0; j < _equipmentGrid.GetLength(1); j++)
                {
                    _equipmentGrid[i, j].SetDefaultColor();
                }
            }
        }

        private static void SetEquipmentTilesColor(bool suitable, List<Vector2Int> equipmentSlotsUnderDragAndDropObjectArrayIndexes, Vector2Int itemSize)
        {
            foreach (Vector2Int arrayIndex in equipmentSlotsUnderDragAndDropObjectArrayIndexes)
            {
                for (int i = arrayIndex.x; i < itemSize.x + arrayIndex.x - 1; i++)
                {
                    for (int j = arrayIndex.y; j < itemSize.y + arrayIndex.y - 1; j++)
                    {
                        Color newColor = suitable ? _itemSuitableColor : _itemUnsuitableColor;
                        try
                        {
                            _equipmentGrid[i, j].SetColor(newColor);
                        }
                        catch { }
                    }
                }
            }
        }

        #endregion
    }
}
