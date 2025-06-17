using UnityEngine;
using System.Collections;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] private GameObject trashPrefab; 
    [SerializeField] private Transform[] lanes; 
    [SerializeField] private float baseSpawnInterval = 1.5f; 
    [SerializeField] private float minSpawnInterval = 0.3f; 
    [SerializeField] private float scoreFactor = 0.05f; 
    [SerializeField] private float spawnHeightOffset = 6f; 
    private float currentSpawnInterval;
    private int lastScore;

    void Start()
    {
        UpdateSpawnInterval();
        StartCoroutine(SpawnTrashRoutine());
    }

    void Update()
    {
        if (BasketController.Score != lastScore)
        {
            UpdateSpawnInterval();
        }
    }

    private void UpdateSpawnInterval()
    {
        currentSpawnInterval = Mathf.Max(minSpawnInterval, baseSpawnInterval / (1f + BasketController.Score * scoreFactor));
        lastScore = BasketController.Score;
    }

    private IEnumerator SpawnTrashRoutine()
    {
        while (true)
        {
            int laneIndex = Random.Range(0, lanes.Length);
            Vector3 spawnPos = lanes[laneIndex].position + Vector3.up * spawnHeightOffset;
            Instantiate(trashPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }
}