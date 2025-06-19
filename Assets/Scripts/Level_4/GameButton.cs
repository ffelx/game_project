using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private BasketController _basket;
    [SerializeField] private bool _isLeft;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isLeft)
        {
            _basket.MoveLeft();
            return;
        }
        _basket.MoveRight();
    }
}
