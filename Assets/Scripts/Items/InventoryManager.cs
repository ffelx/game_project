using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

namespace Assets.Scripts.Items
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        private List<Item> _items = new List<Item>();
        public int Count { get { return _items.Count; } }

        [SerializeField] private string saveFileName = "inventory.json";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadInventory();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddItem(Item item)
        {
            Debug.Log(item != null);
            Debug.Log(_items != null);

            foreach (var item1 in _items)
            {
                Debug.Log(item1.Name + " - " + item1.Text);
            }

            if (!_items.Any(i => i.Name == item.Name))
            {
                _items.Add(item);
                SaveInventory();
            }
            
        }

        public List<Item> GetItems()
        {
            return _items;
        }

        public void SaveInventory()
        {
            Directory.CreateDirectory(Application.persistentDataPath); 

            var wrapper = new InventoryWrapper { items = _items };
            string json = JsonUtility.ToJson(wrapper, true);

            string path = Path.Combine(Application.persistentDataPath, saveFileName);
            File.WriteAllText(path, json);
        }

        public void LoadInventory()
        {
            string path = Path.Combine(Application.persistentDataPath, saveFileName);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var wrapper = JsonUtility.FromJson<InventoryWrapper>(json);
                if (wrapper != null)
                {
                    if (wrapper.items != null)
                    {
                        _items = wrapper.items;
                    }
                    else
                    {
                        _items = new List<Item>();
                    }
                }
                else
                {
                    _items = new List<Item>();
                }
            }
            else
            {
                _items = new List<Item>(); 
            }
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeAutomatically()
        {
            GameObject inventoryManager = new GameObject("InventoryManager");
            inventoryManager.AddComponent<InventoryManager>();
        }


        [System.Serializable]
        private class InventoryWrapper
        {
            public List<Item> items;
        }
    }
}
