using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OverMars
{
    public class DragAndDropContainer : Singleton<DragAndDropContainer>
    {
        public static SlotUI SlotUnderCursor;

        [SerializeField] private Image _dragAndDropItem;

        private EquipmentItem _itemInContainer;

        public void UpdateDragAndDropContainerPosition()
        {
            if (_itemInContainer != null)
            {
                _dragAndDropItem.transform.position = Input.mousePosition;
            }
        }

        public void AddItemToContainer(EquipmentItem item)
        {
            if (_itemInContainer != null)
            {
                Debug.LogError("There is already an item in Drag And Drop Container!");
            }

            _itemInContainer = item;
            _dragAndDropItem.sprite = item.Sprite;
            _dragAndDropItem.enabled = true;
        }

        public void RemoveItemFromContainer()
        {
            _itemInContainer = null;
            _dragAndDropItem.enabled = false;
            _dragAndDropItem.sprite = null;
        }

        public EquipmentItem GetItemInContainer()
        {
            EquipmentItem itemInContainer = _itemInContainer;
            RemoveItemFromContainer();
            return itemInContainer;
        }
    }
}
