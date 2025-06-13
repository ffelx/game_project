using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private Image image;
    public int x, y;
    public bool isSoil = true;
    public bool hasTree = false;

    public delegate void CellClicked(Cell cell);
    public event CellClicked onClick;

    public void Init(int x, int y, Color color)
    {
        this.x = x;
        this.y = y;
        GetComponent<Image>().color = color;
        GetComponent<Button>().onClick.AddListener(() => onClick?.Invoke(this));
        image = GetComponent<Image>();
    }

    public void SetTree(Color color)
    {
        hasTree = true;
        image.color = color;
    }

    public void SetSand(Color color)
    {
        isSoil = false;
        image.color = color;
        GetComponent<Button>().interactable = false;
    }
}