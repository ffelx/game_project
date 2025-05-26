using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterScript : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        if (InventoryManager.Instance != null)
        {
            var item = new Item() { Name = _name, Text = _text.text};
            Debug.Log(item.Name);
            InventoryManager.Instance.AddItem(item);
            Debug.Log("По идее сохранился");
            Debug.Log("Путь сохранения: " + Application.persistentDataPath);
            return;
        }
        Debug.Log("InventoryManager не создан");
        
    }
}
