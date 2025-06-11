using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BoardUI : MonoBehaviour
{
    [SerializeField] private Transform contentParent; 
    [SerializeField] private GameObject itemSlotPrefab; 
    [SerializeField] private string itemsFolder = "Items"; 
    private List<ItemSlot> _spawnedSlots = new List<ItemSlot>();

    [SerializeField] private Sprite _itemSprite;

    public void Show()
    {
        Clear();
        foreach (var item in InventoryManager.Instance.GetItems())
        {
            //Sprite sprite = Resources.Load<Sprite>(Path.Combine(itemsFolder, item.Name));
            if (_itemSprite == null)
            {
                Debug.LogWarning($"Спрайт не найден для {item.Name}");
                continue;
            }

            GameObject go = Instantiate(itemSlotPrefab, contentParent);
            ItemSlot slot = go.GetComponent<ItemSlot>();
            slot.SetItem(item, _itemSprite);
            _spawnedSlots.Add(slot);
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Clear();
        gameObject.SetActive(false);
    }

    private void Clear()
    {
        foreach (var slot in _spawnedSlots)
        {
            Destroy(slot.gameObject);
        }
        _spawnedSlots.Clear();
    }
}
