using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace OverMars
{
    public abstract class SlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [HideInInspector] public EquipmentItem EquipmentItem;

        private RectTransform _imageRect;
        private Vector2Int _defaultTileImageSize;
        private protected Image _image;
        private Sprite _emptySlotSprite;
        private protected Color _emptySlotColor;

        public virtual int Id => -1;
        public virtual Vector2Int ArrayIndexes => new Vector2Int(-1, -1);
        public abstract bool IsEquipmentSlot { get; }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _imageRect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _emptySlotSprite = _image.sprite;
            _emptySlotColor = _image.color;
            _defaultTileImageSize = new Vector2Int((int)_imageRect.sizeDelta.x, (int)_imageRect.sizeDelta.y);
        }

        public abstract void SetItem(EquipmentItem equipmentItem);

        private protected abstract void RemoveItem();

        private protected void UpdateSlotUI()
        {
            if (EquipmentItem)
            {
                _image.sprite = EquipmentItem.Sprite;
                _image.color = Color.white;
                _imageRect.sizeDelta = EquipmentItem.Sprite.rect.size;
            }
            else
            {
                _image.sprite = _emptySlotSprite;
                _image.color = _emptySlotColor;
                _imageRect.sizeDelta = _defaultTileImageSize;
            }

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
            if (slotUnderCursor && slotUnderCursor.IsEquipmentSlot)
            {
                EquipmentPanelController.SetSlotsUnderItemAreNotEmpty();
                slotUnderCursor.SetItem(itemInContainer);
            }

            EquipmentPanelController.MarkTilesAsNotUnderItem();
        }
    }
}
