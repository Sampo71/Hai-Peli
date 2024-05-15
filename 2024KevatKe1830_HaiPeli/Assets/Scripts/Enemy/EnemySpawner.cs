using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadius = 15f;
    public float spawnInterval = 1f;
    private float nextSpawnTime;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < nextSpawnTime){
            return;
        }
        SpawnEnemy();
    }

    private void SpawnEnemy(){
        if(playerTransform == null){
            GetPlayer();
        }
        Vector2 spawnPos = playerTransform.position;
        spawnPos += Random.insideUnitCircle.normalized *spawnRadius;

        GameObject enemy = EnemyPoolManager.Instance.GetEnemy();
        enemy.transform.position = spawnPos;
        nextSpawnTime = Time.time + spawnInterval;
    }
    
    void GetPlayer()
    {
        playerTransform = GameManager.Instance.getPlayer.transform;
    }
}
