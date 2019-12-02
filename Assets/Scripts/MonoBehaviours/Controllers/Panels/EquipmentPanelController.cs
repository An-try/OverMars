using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OverMars
{
    public class EquipmentPanelController : PanelCommon<EquipmentPanelController>
    {
#pragma warning disable 0649

        [SerializeField] private ShipItem _shipItem;
        [SerializeField] private Transform _shipTilesContainerUI;
        [SerializeField] private Image _shipImageComponent;
        [SerializeField] private GameObject _shipTileUIPrefab;
        [SerializeField] private RectTransform _equipmentContentRect;
        [SerializeField] private RectTransform _backgroundRect;

        [Header("References for scaling")]
        [SerializeField] private ScrollRect _equipmentScrollRect;
        [SerializeField] private Transform[] _objectsToScale;

#pragma warning restore 0649

        public static EquipmentSlotUI[,] EquipmentTilesGrid;
        public static List<Vector2Int> EquipmentSlotsUnderDragAndDropObjectArrayIndexes = new List<Vector2Int>();
        public static bool IsItemInDragAndDropSuitable = false;

        private static Color _itemSuitableColor = Color.green;
        private static Color _itemUnsuitableColor = Color.red;

        #region Fields for scaling

        private int _currentTouchesCount = 0;

        private float _zoomSensitivity = 2f;
        private float _previousTouchesDelta = 0f;
        private float _currentTouchesDelta = 0f;

        private bool _alreadyPressed = false;

        private const float MOUSE_SCROLL_MULTIPLIER = 2;
        private const float MIN_SCALE = 0.15f;
        private const float MAX_SCALE = 1f;

        #endregion

        private void Start()
        {
            RebuildTiles();
        }

        private void Update()
        {
#if UNITY_EDITOR
            ScaleObjectsWithMouse();
#else
            ScaleObjectsWithFingers();
#endif

            ClampEquipmentContentPosition();
            MoveBackgroundByEquipmentTiles();
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

            _shipImageComponent.sprite = _shipItem.Sprite;
            _shipImageComponent.rectTransform.sizeDelta = new Vector2(_shipItem.Sprite.rect.size.x, _shipItem.Sprite.rect.size.y);

            string cleanTilesCode = _shipItem.CleanTilesCode;
            int shipWidth = _shipItem.Width;
            int shipHeight = _shipItem.Height;

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

        public static void CheckDragAndDropItemForSuitability(List<Vector2Int> equipmentSlotsUnderDragAndDropObjectArrayIndexes, int itemWidth, int itemHeight)
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
            MarkTilesAsUnderItem(IsItemInDragAndDropSuitable, itemWidth, itemHeight);
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

        private static void MarkTilesAsUnderItem(bool suitable, int itemWidth, int itemHeight)
        {
            foreach (Vector2Int arrayIndex in EquipmentSlotsUnderDragAndDropObjectArrayIndexes)
            {
                if (arrayIndex.x < EquipmentTilesGrid.GetLength(0) && arrayIndex.y < EquipmentTilesGrid.GetLength(1))
                {
                    Color newColor = suitable ? _itemSuitableColor : _itemUnsuitableColor;
                    EquipmentTilesGrid[arrayIndex.x, arrayIndex.y].MarkAsUnderItem(newColor);
                }
            }
        }

        #endregion


        #region Scaling equipment content

        private void ScaleObjectsWithMouse()
        {
            float mouseScroll = Input.mouseScrollDelta.y * _zoomSensitivity * MOUSE_SCROLL_MULTIPLIER * Time.deltaTime;
            Vector3 mouseWorldPosition =
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));

            if (MouseInQuarter(mouseWorldPosition) && mouseScroll != 0)
            {
                ZoomImage(mouseScroll);
            }
        }

        private void ScaleObjectsWithFingers()
        {
            if (Input.touchCount >= 2 && AllFingersAreInFourthQuarter())
            {
                if (_currentTouchesCount != Input.touchCount)
                {
                    _previousTouchesDelta = GetDeltaMagnitudeDifference();
                    _currentTouchesCount = Input.touchCount;
                }

                if (!_alreadyPressed)
                {
                    _previousTouchesDelta = GetDeltaMagnitudeDifference();
                    _alreadyPressed = true;
                }

                _equipmentScrollRect.enabled = false;

                _currentTouchesDelta = GetDeltaMagnitudeDifference();
                float newScale = ((_currentTouchesDelta / _previousTouchesDelta) - 1) * _zoomSensitivity;
                _previousTouchesDelta = _currentTouchesDelta;

                ZoomImage(newScale);
            }
            else if (Input.touchCount == 1)
            {
                _alreadyPressed = false;
                _equipmentScrollRect.enabled = true;
            }
            else
            {
                _alreadyPressed = false;
            }
        }

        private void ZoomImage(float zoomingValue)
        {
            foreach (Transform objectToScale in _objectsToScale)
            {
                objectToScale.localScale += new Vector3(zoomingValue, zoomingValue, zoomingValue);

                objectToScale.localScale = new Vector3(Mathf.Clamp(objectToScale.localScale.x, MIN_SCALE, MAX_SCALE),
                                                       Mathf.Clamp(objectToScale.localScale.y, MIN_SCALE, MAX_SCALE),
                                                       Mathf.Clamp(objectToScale.localScale.z, MIN_SCALE, MAX_SCALE));
            }
        }

        private bool AllFingersAreInFourthQuarter()
        {
            //foreach (Touch touch in Input.touches)
            //{
            //    Vector3 touchWorldCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100));

            //    if (touchWorldCoordinates.x < _resizeButton.transform.position.x || touchWorldCoordinates.y > _resizeButton.transform.position.y)
            //    {
            //        return false;
            //    }
            //    return true;
            //}
            return true;
        }

        private bool MouseInQuarter(Vector3 mouseWorldPosition)
        {
            //if (mouseWorldPosition.x < _resizeButton.transform.position.x ||
            //    mouseWorldPosition.y > _resizeButton.transform.position.y)
            //{
            //    return false;
            //}
            return true;
        }

        private float GetDeltaMagnitudeDifference()
        {
            float result = 0;
            for (int i = 0; i < Input.touches.Length - 1; ++i)
            {
                for (int j = i + 1; j < Input.touches.Length; ++j)
                {
                    result += Vector2.SqrMagnitude(Input.touches[i].position - Input.touches[j].position);
                }
            }
            return result / (Input.touches.Length * Input.touches.Length);
        }

        #endregion


        #region Clamping equipment content position

        private void ClampEquipmentContentPosition()
        {
            _equipmentContentRect.anchoredPosition = new Vector2(Mathf.Clamp(_equipmentContentRect.anchoredPosition.x, -Screen.width / 2, Screen.width / 2),
                                                                 Mathf.Clamp(_equipmentContentRect.anchoredPosition.y, -Screen.height / 2, Screen.height / 2));
        }

        #endregion


        #region Moving background by equipment tiles

        private void MoveBackgroundByEquipmentTiles()
        {
            _backgroundRect.anchoredPosition = _equipmentContentRect.anchoredPosition * 0.05f;
        }

        #endregion
    }
}
