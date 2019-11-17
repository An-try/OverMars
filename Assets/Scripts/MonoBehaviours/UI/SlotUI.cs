using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace OverMars
{
    public abstract class SlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [HideInInspector] public EquipmentItem EquipmentItem;

        private protected Image _image;
        
        private Sprite _emptySlotSprite;
        private Color _emptySlotColor;

        public abstract bool IsEquipmentSlot { get; }

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _emptySlotSprite = _image.sprite;
            _emptySlotColor = _image.color;
        }

        public abstract void SetItem(EquipmentItem equipmentItem);

        private protected abstract void RemoveItem();

        private protected void UpdateSlotUI()
        {
            _image.sprite = EquipmentItem ? EquipmentItem.Sprite : _emptySlotSprite;
            _image.color = EquipmentItem ? Color.white : _emptySlotColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            DragAndDropController.SlotUnderCursor = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (DragAndDropController.SlotUnderCursor == this)
            {
                DragAndDropController.SlotUnderCursor = null;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (this.EquipmentItem)
            {
                DragAndDropController.AddItemToContainer(this.EquipmentItem);
                this.RemoveItem();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            DragAndDropController.UpdateDragAndDropContainerPosition();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EquipmentItem itemInContainer = DragAndDropController.GetItemInContainer();
            if (!itemInContainer)
            {
                return;
            }

            SlotUI slotUnderCursor = DragAndDropController.SlotUnderCursor;
            if (!slotUnderCursor)
            {
                if (this.IsEquipmentSlot)
                {
                    this.RemoveItem();
                }
                return;
            }

            if (slotUnderCursor.IsEquipmentSlot)
            {
                slotUnderCursor.SetItem(itemInContainer);
            }
        }

        //private void ReplaceItems(SlotUI slotUnderCursor, EquipmentItem itemInContainer)
        //{
        //    SetItem(slotUnderCursor.EquipmentItem);
        //    slotUnderCursor.SetItem(itemInContainer);
        //}

        //private void ReturnItem(SlotUI slotUnderCursor, EquipmentItem itemInContainer)
        //{
        //    slotUnderCursor.SetItem(itemInContainer);
        //}
    }
}
