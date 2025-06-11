using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Transform contentParent; 
    public GameObject itemSlotPrefab; 
    public List<ItemData> items = new List<ItemData>();

    public void LoadItems()
    {
        //foreach (var item in items)
        //{
        //    GameObject go = Instantiate(itemSlotPrefab, contentParent);
        //    ItemSlot slot = go.GetComponent<ItemSlot>();
        //    Sprite sprite = Resources.Load<Sprite>(item.spritePath);
        //    slot.SetItem(sprite, item.description);
        //}
    }

    [System.Serializable]
    public class ItemData
    {
        public string name;
        public string spritePath; // Путь относительно Resources/
        public string description;
    }
}
