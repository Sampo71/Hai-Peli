using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;
    public int poolSize = 10;
    public GameObject EnemyPrefab;
    private Queue<GameObject> enemyPool = new Queue<GameObject>();

    void Awake(){
        Instance = this;
    }

    void InitializePool(){
        for(int i = 0; i < poolSize; i++){
          GameObject newEnemy = Instantiate(EnemyPrefab);
          newEnemy.SetActive(false);
          enemyPool.Enqueue(newEnemy);
        }
    }

    public GameObject GetEnemy(){
        if(enemyPool.Count > 0){
           GameObject enemy = enemyPool.Dequeue();
           enemy.SetActive(true);
           return enemy;
        }
        else{
            GameObject newEnemy = Instantiate(EnemyPrefab);
            newEnemy.SetActive(true);
            return newEnemy;
        }        
    }

    public void ReturnEnemy(GameObject enemy){
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
