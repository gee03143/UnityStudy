using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    [System.Serializable]
    public class Enemy
    {
        public string name;
        public int maxCount;
        public float spawnDelay;
    }

    public Enemy[] enemies;
    private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public float timeBetweenWaves = 5f;


    private SpawnState state = SpawnState.COUNTING;
    public SpawnState State
    {
        get { return state; }
    }

    private void Start()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            StartCoroutine(SpawnEnemys(i));
        }
    }

    IEnumerator SpawnEnemys(int id)
    {
        while (true)
        {
            SpawnEnemy(id);
            yield return new WaitForSeconds(enemies[id].spawnDelay);
        }
    }

    void SpawnEnemy(int id)
    {
        if (enemies[id].maxCount > PoolManager.instance.enemies[id].activePool.Count)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-GM.ScreenWidth * 0.5f, GM.ScreenWidth * 0.5f), GM.ScreenHeight * 0.6f);
            EnemyBase enemy = PoolManager.instance.GetEnemy(id);
            enemy.transform.position = spawnPos;
            enemy.gameObject.SetActive(true);
        }
    }
}
