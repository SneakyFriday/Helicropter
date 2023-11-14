using UnityEngine;
using Random = UnityEngine.Random;

public class CornSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cornPrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnDistance = 1f;

    private float spawnTimer = 0f;
    private float spawnTimerMax = 0f;
    private Vector3 spawnPosition = Vector3.zero;

    private ObjectPool<GameObject> cornPool;

    private void Start()
    {
        spawnTimerMax = spawnInterval;
        cornPool = new ObjectPool<GameObject>(cornPrefab); // Pass the cornPrefab here
    }

    private void Update()
    {
        if (!GameManager.IsGameRunning) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTimerMax)
        {
            spawnTimer -= spawnTimerMax;
            spawnTimerMax = Random.Range(spawnInterval * 0.5f, spawnInterval * 1.5f);
            spawnPosition = transform.position + Vector3.right * spawnDistance;

            GameObject cornObject = cornPool.GetObject(transform);
            cornObject.transform.position = spawnPosition;
        }
    }

    private void LateUpdate()
    {
        spawnTimerMax = Random.Range(spawnInterval * 0.2f, spawnInterval * 2f);
        spawnDistance = Random.Range(0.5f, 2f);
    }
}
