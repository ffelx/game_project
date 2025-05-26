using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Text itemNameText;

    public void Setup(Item item)
    {
        if (itemNameText != null && item != null)
        {
            itemNameText.text = item.Name;
        }
    }
}
