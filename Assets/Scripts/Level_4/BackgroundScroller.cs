using System;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float _speed = 2f; 
    [SerializeField] private float _spawnDistance = 5f; 
    [SerializeField] private float _spawnHeightOffset = 10f; 
    [SerializeField] private float _destroyHeightOffset = 10f; 
    private static Vector3 _globalStartPos; 
    private float _repeatY; 
    private bool _hasSpawnedCopy = false; 

    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            return;
        }

        if (_globalStartPos == Vector3.zero)
        {
            _globalStartPos = transform.position;
          
        }
        _repeatY = _globalStartPos.y - _spawnDistance;
    }

    void Update()
    {
        _speed = Trash.speed;
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);


        if (transform.position.y <= _repeatY && !_hasSpawnedCopy)
        {
            Vector3 spawnPos = new Vector3(_globalStartPos.x, _globalStartPos.y + _spawnHeightOffset, _globalStartPos.z);
            GameObject newBackground = Instantiate(gameObject, spawnPos, Quaternion.Euler(0f, 0f, 90f));
            _hasSpawnedCopy = true;
        }

        if (transform.position.y <= _globalStartPos.y - _destroyHeightOffset)
        {
            Destroy(gameObject);
        }
    }
}
