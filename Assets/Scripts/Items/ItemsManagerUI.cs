using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private Transform contentPanel; 
    [SerializeField] private GameObject itemSlotPrefab; 

    public void Awake()
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        List<Item> items = InventoryManager.Instance.GetItems();

        foreach (var item in items)
        {
            GameObject slotGO = Instantiate(itemSlotPrefab, contentPanel);
            ItemSlotUI slot = slotGO.GetComponent<ItemSlotUI>();
            if (slot != null)
            {
                slot.Setup(item);
            }
        }
    }
}
