using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace OverMars
{
    public class DragAndDropController : Singleton<DragAndDropController>
    {
        public static SlotUI SlotUnderCursor;

        [SerializeField] private GameObject _dragAndDropOblect;
        private static CanvasGroup _dragAndDropGroup;
        private static Image _dragAndDropImage;

        private static EquipmentItem _itemInContainer;

        private List<Vector2Int> _equipmentSlotsUnderDragAndDropObjectArrayIndexes = new List<Vector2Int>();

        private protected override void Awake()
        {
            base.Awake();
            _dragAndDropGroup = _dragAndDropOblect.GetComponent<CanvasGroup>();
            _dragAndDropImage = _dragAndDropOblect.GetComponent<Image>();
        }

        private void Update()
        {
            DetermineSlotsUIAboveWhichTheDragAndDropObjectIs();
        }

        private void DetermineSlotsUIAboveWhichTheDragAndDropObjectIs()
        {
            if (_itemInContainer && SlotUnderCursor && SlotUnderCursor.IsEquipmentSlot)
            {
                Vector2Int itemSize = _itemInContainer.Size;
                Vector2Int equipmentSlotArrayIndexes = SlotUnderCursor.ArrayIndexes;
                _equipmentSlotsUnderDragAndDropObjectArrayIndexes = new List<Vector2Int>();

                for (int i = 0; i < itemSize.x; i++)
                {
                    for (int j = 0; j < itemSize.y; j++)
                    {
                        _equipmentSlotsUnderDragAndDropObjectArrayIndexes.Add(new Vector2Int(equipmentSlotArrayIndexes.x + i, equipmentSlotArrayIndexes.y + j));
                    }
                }

                EquipmentPanelController.CheckDragAndDropItemForSuitability(_equipmentSlotsUnderDragAndDropObjectArrayIndexes, itemSize);
            }
            else
            {
                EquipmentPanelController.MarkTilesAsNotUnderItem();
            }
        }

        public static void UpdateDragAndDropContainerPosition()
        {
            if (_itemInContainer != null)
            {
                _dragAndDropGroup.transform.position = Input.mousePosition;
            }
        }

        public static void AddItemToContainer(EquipmentItem item)
        {
            if (_itemInContainer != null)
            {
                Debug.LogError("There is already an item in Drag And Drop Container!");
            }

            _itemInContainer = item;
            _dragAndDropGroup.transform.GetComponent<RectTransform>().sizeDelta = item.Sprite.rect.size;// * !!! SHIP_CURRENT_SIZE_MULTIPLIER !!!
            _dragAndDropImage.sprite = item.Sprite;
            _dragAndDropGroup.alpha = 1;
        }

        public static void RemoveItemFromContainer()
        {
            _itemInContainer = null;
            _dragAndDropGroup.alpha = 0;
            _dragAndDropImage.sprite = null;
        }

        public static EquipmentItem GetItemInContainer()
        {
            EquipmentItem itemInContainer = _itemInContainer;
            RemoveItemFromContainer();
            return itemInContainer;
        }
    }
}
