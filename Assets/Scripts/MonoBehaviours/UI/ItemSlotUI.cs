namespace OverMars
{
    public class ItemSlotUI : SlotUI
    {
        public override bool IsEquipmentSlot => false;

        public override void SetItem(EquipmentItem equipmentItem)
        {
            EquipmentItem = equipmentItem;
            UpdateSlotUI();
        }

        private protected override void RemoveItem()
        {

        }
    }
}
