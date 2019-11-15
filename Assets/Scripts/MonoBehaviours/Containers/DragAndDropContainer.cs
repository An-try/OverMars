using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OverMars
{
    public class DragAndDropContainer : Singleton<DragAndDropContainer>
    {
        public static SlotUI SlotUnderCursor;

        [SerializeField] private Image _dragAndDropObject;

        private EquipmentItem _itemInContainer;

        public void UpdateDragAndDropContainerPosition()
        {
            if (_itemInContainer != null)
            {
                _dragAndDropObject.transform.position = Input.mousePosition;
            }
        }

        public void AddItemToContainer(EquipmentItem item)
        {
            if (_itemInContainer != null)
            {
                Debug.LogError("There is already an item in Drag And Drop Container!");
            }

            _itemInContainer = item;
            _dragAndDropObject.transform.GetComponent<RectTransform>().sizeDelta = item.Sprite.rect.size;
            _dragAndDropObject.sprite = item.Sprite;
            _dragAndDropObject.enabled = true;
        }

        public void RemoveItemFromContainer()
        {
            _itemInContainer = null;
            _dragAndDropObject.enabled = false;
            _dragAndDropObject.sprite = null;
        }

        public EquipmentItem GetItemInContainer()
        {
            EquipmentItem itemInContainer = _itemInContainer;
            RemoveItemFromContainer();
            return itemInContainer;
        }
    }
}
