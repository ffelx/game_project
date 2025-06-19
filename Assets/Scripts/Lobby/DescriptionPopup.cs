using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionPopup : MonoBehaviour
{
    public static DescriptionPopup Instance;

    [SerializeField] private GameObject popupPanel;
    [SerializeField] private Text itemText;

    [SerializeField] private Image itemImage;
    [SerializeField] private Image blackBackground;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            itemImage.color = Color.white;
            blackBackground.color = new Color(0, 0, 0, 0.8f);    
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        this.gameObject.SetActive(false);
    }

    public void Show(Item item, Sprite sprite = null)
    {
        //itemIcon.sprite = sprite;
        itemText.text = item.Text;
        popupPanel.SetActive(true);
    }

    public void Hide()
    {
        popupPanel.SetActive(false);
    }
}
