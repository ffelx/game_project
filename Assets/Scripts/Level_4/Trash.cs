using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    public static float speed;
    public static int countMissedTrash;

    [SerializeField] private List<Sprite> randomSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (randomSprites != null && randomSprites.Count > 0)
        {
            int index = Random.Range(0, randomSprites.Count);
            spriteRenderer.sprite = randomSprites[index];
        }
    }


    void Update()
    {
        float baseSpeed = 2f;
        float minSpeed = 1f;
        float maxSpeed = 10f;
        float speedFactor = 0.2f;
        _speed = BasketController.Score <= 0 ? minSpeed : Mathf.Min(maxSpeed, baseSpeed * (1f + BasketController.Score * speedFactor));
        speed = _speed;
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);

        if (transform.position.y < -6f)
        {
            TrashMiss();
        }
    }

    public void TrashMiss()
    {
        BasketController.Score -= 5;
        countMissedTrash += 1;
        if (BasketController.Score < 0)
        {
            BasketController.Score = 0;
        }
        if (countMissedTrash == 1 || (countMissedTrash % 5 == 0 && countMissedTrash != 0))
        {
            var helper = FindObjectOfType<Helper>();
            helper.ShowBadDialog();
        }
        Destroy(gameObject);
       
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            //BasketController.Score += 1; 
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
        Debug.Log($"Скорость мусора: {_speed:F2}");
    }
}