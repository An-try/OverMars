using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public class ItemsPanelController : PanelCommon<ItemsPanelController>
    {
#pragma warning disable 0649

        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private GameObject _itemSlotUIPrefab;

#pragma warning restore 0649

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
                itemSlotUI.SetItem(ItemsContainer.Instance.AllEquipmentItems[i], new List<Vector2Int>());
            }
        }
    }
}
