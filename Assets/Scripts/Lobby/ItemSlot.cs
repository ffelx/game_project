using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Text ItemText;

    private Item _item;

    public void SetItem(Item item, Sprite sprite)
    {
        _item = item;

        if (iconImage != null && sprite != null)
        {
            iconImage.sprite = sprite;
        }

        //if (itemButton != null)
        //{
        //    itemButton.onClick.RemoveAllListeners();
        //    itemButton.onClick.AddListener(OnItemClick);
        //}
    } 

    public void OnItemClick()
    {
        DescriptionPopup.Instance.Show(_item);
    }
}
