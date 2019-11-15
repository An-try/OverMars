using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public class ItemsPanelController : PanelCommon<ItemsPanelController>
    {
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private GameObject _itemSlotUIPrefab;

        private void Start()
        {
            OpenPanel();
            RepopulateItemsList();
        }

        private void RepopulateItemsList()
        {
            for (int i = 0; i < ItemsContainer.Instance.AllEquipmentItems.Count; i++)
            {
                ItemSlotUI itemSlotUI = Instantiate(_itemSlotUIPrefab, _itemsContainer).GetComponent<ItemSlotUI>();
                itemSlotUI.SetItem(ItemsContainer.Instance.AllEquipmentItems[i]);
            }
        }
    }
}
