using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    public static float speed;

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
            Destroy(gameObject);
            BasketController.Score -= 5;
            if (BasketController.Score < 0)
            {
                BasketController.Score = 0;
            }
        }
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