using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace OverMars
{
    public class DragAndDropController : Singleton<DragAndDropController>
    {
        public static SlotUI SlotUnderCursor;

        [SerializeField] private CanvasGroup _dragAndDropGroup;
        [SerializeField] private Image _dragAndDropImage;

        private EquipmentItem _itemInContainer;

        private static List<RaycastResult> raycastResults = new List<RaycastResult>();

        private void Update()
        {
            DetermineSlotUIAboveWhichTheArrowIs();
        }

        private void DetermineSlotUIAboveWhichTheArrowIs()
        {
            if (Input.touches.Length > 0 || Input.GetMouseButton(0))
            {
                GraphicRaycaster graphicRaycaster = new GraphicRaycaster();
                graphicRaycaster.Raycast(new PointerEventData(EventSystem.current), raycastResults);
                SlotUnderCursor
            }
        }

        public void UpdateDragAndDropContainerPosition()
        {
            if (_itemInContainer != null)
            {
                _dragAndDropGroup.transform.position = Input.mousePosition;
            }
        }

        public void AddItemToContainer(EquipmentItem item)
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

        public void RemoveItemFromContainer()
        {
            _itemInContainer = null;
            _dragAndDropGroup.alpha = 1;
            _dragAndDropImage.sprite = null;
        }

        public EquipmentItem GetItemInContainer()
        {
            EquipmentItem itemInContainer = _itemInContainer;
            RemoveItemFromContainer();
            return itemInContainer;
        }
    }
}
