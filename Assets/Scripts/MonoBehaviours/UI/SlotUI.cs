using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace OverMars
{
    public abstract class SlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Image _image;

        [HideInInspector] public EquipmentItem EquipmentItem;
        public abstract bool IsInventorySlot { get; }

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private protected abstract void RemoveItem();

        private protected void UpdateSlotUI(Item item)
        {
            _image.sprite = item.Sprite;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            DragAndDropContainer.SlotUnderCursor = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DragAndDropContainer.SlotUnderCursor = null;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DragAndDropContainer.Instance.AddItemToContainer(this.EquipmentItem);
        }

        public void OnDrag(PointerEventData eventData)
        {
            DragAndDropContainer.Instance.UpdateDragAndDropContainerPosition();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EquipmentItem itemInContainer = DragAndDropContainer.Instance.GetItemInContainer();
            if (!itemInContainer)
            {
                return;
            }

            SlotUI slotUnderCursor = DragAndDropContainer.SlotUnderCursor;
            EquipmentItem itemUnderCursor = null;
            if (slotUnderCursor && slotUnderCursor.EquipmentItem)
            {
                itemUnderCursor = slotUnderCursor.EquipmentItem;
            }

            if (!slotUnderCursor)
            {
                ReturnItem(this, itemInContainer);
                return;
            }

            if (!slotUnderCursor.IsInventorySlot && !itemInContainer.IsEquipment)
            {
                ReturnItem(this, itemInContainer);
                return;
            }

            if (!this.IsInventorySlot && itemUnderCursor && !itemUnderCursor.IsEquipment)
            {
                ReturnItem(this, itemInContainer);
                return;
            }

            ReplaceItems(slotUnderCursor, itemInContainer);
        }

        public abstract void SetItem(EquipmentItem equipmentItem);

        private void ReplaceItems(SlotUI slotUnderCursor, EquipmentItem itemInContainer)
        {
            SetItem(slotUnderCursor.EquipmentItem);
            slotUnderCursor.SetItem(itemInContainer);
        }

        private void ReturnItem(SlotUI slotUnderCursor, EquipmentItem itemInContainer)
        {
            slotUnderCursor.SetItem(itemInContainer);
        }
    }
}
