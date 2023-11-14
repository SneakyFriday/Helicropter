using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cornPrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnDistance = 1f;
    
    private float spawnTimer = 0f;
    private float spawnTimerMax = 0f;
    private Vector3 spawnPosition = Vector3.zero;
    
    void Start()
    {
        spawnTimerMax = spawnInterval;
    }
    
    void Update()
    {
        if(GameManager.IsGameRunning == false) return;
        spawnTimer += Time.deltaTime;
        
        if (!(spawnTimer >= spawnTimerMax)) return;
        spawnTimer -= spawnTimerMax;
        spawnTimerMax = Random.Range(spawnInterval * 0.5f, spawnInterval * 1.5f);
        // Make Spawn Position Random in Y
        spawnPosition = transform.position + Vector3.right * spawnDistance + Vector3.up * Random.Range(-2f, 2f);
        Instantiate(cornPrefab, spawnPosition, Quaternion.identity);
    }
}
