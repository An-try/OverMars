namespace OverMars
{
    public class ItemSlotUI : SlotUI
    {
        public override bool IsInventorySlot => true;

        public override void SetItem(EquipmentItem equipmentItem)
        {
            EquipmentItem = equipmentItem;
            UpdateSlotUI(equipmentItem);
        }

        private protected override void RemoveItem()
        {

        }
    }
}
