using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyData
    {
        public GameObject[] enemyPrefabs;
        public int maxCount;
        public LinkedList<EnemyBase> pool = new LinkedList<EnemyBase>();
        public LinkedList<EnemyBase> activePool = new LinkedList<EnemyBase>();
    }

    public static PoolManager instance;
    GameObject poolRoot;

    //public LinkedList<EnemyBase> enemyPool = new LinkedList<EnemyBase>();


    public LinkedList<PlayerBullet> playerBulletPool = new LinkedList<PlayerBullet>();
    public LinkedList<PlayerBullet> activeBullets = new LinkedList<PlayerBullet>();
    public GameObject playerBulletPrefab;
    private const int POOL_COUNT = 6;

    public LinkedList<Bullet> enemyBulletPool = new LinkedList<Bullet>();
    public LinkedList<Bullet> activeEnemyBullets = new LinkedList<Bullet>();
    public GameObject enemyBulletPrefab;
    private const int ENEMY_BULLET_COUNT = 30;

    public GameObject enemyDeathEffect;


    public EnemyData[] enemies;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        Debug.Log("오브젝트 풀 생성됨");
        FindRoot();
        MakePlayerBullet();
        MakeEnemyBullet();
        MakeEnemy();
    }

    private void FindRoot()
    {
        poolRoot = GameObject.FindGameObjectWithTag("ObjectPool");
    }

    private void MakePlayerBullet()
    {
        for (int i = 0; i < POOL_COUNT; i++)
        {
            PlayerBullet pb = Instantiate(playerBulletPrefab).GetComponent<PlayerBullet>();
            pb.transform.parent = poolRoot.transform;
            pb.gameObject.SetActive(false);
            playerBulletPool.AddLast(pb);
        }
    }

    public PlayerBullet GetPlayerBullet()
    {
        if (playerBulletPool.Count.Equals(0))
        {
            Debug.Log("오브젝트 풀에 playerbullet 오브젝트가 없습니다.");
        }

        activeBullets.AddLast(playerBulletPool.First.Value);
        playerBulletPool.RemoveFirst();
        return playerBulletPool.First.Value;
    }

    public void ReturnPlayerBullet()
    {
        if (activeBullets.Count.Equals(0))
        {
            return;
        }

        playerBulletPool.AddLast(activeBullets.First.Value);
        activeBullets.RemoveFirst();
    }

    private void MakeEnemyBullet()
    {
        for (int i = 0; i < ENEMY_BULLET_COUNT; i++)
        {
            Bullet ab = Instantiate(enemyBulletPrefab).GetComponent<Bullet>();
            ab.transform.parent = poolRoot.transform;
            ab.gameObject.SetActive(false);
            enemyBulletPool.AddLast(ab);
        }
    }

    public Bullet GetEmenyBullet()
    {
        if (enemyBulletPool.Count.Equals(0))
        {
            Debug.Log("오브젝트 풀에 AimingBullet 오브젝트가 없습니다.");
            MakeEnemyBullet();
        }

        activeEnemyBullets.AddLast(enemyBulletPool.First.Value);
        enemyBulletPool.RemoveFirst();
        return enemyBulletPool.First.Value;
    }

    public void ReturnEnemyBullet()
    {
        if (activeEnemyBullets.Count.Equals(0))
        {
            return;
        }

        enemyBulletPool.AddLast(activeEnemyBullets.First.Value);
        activeEnemyBullets.RemoveFirst();
    }


    public GameObject GetDeathEffect()
    {
        return enemyDeathEffect;
    }

    private void MakeEnemy()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            Debug.Log("다른 종류의 enemy 생성됨");
            for (int j = 0; j < enemies[i].maxCount; j++)
            {
                Debug.Log(enemies[i].enemyPrefabs[Random.Range(0,enemies[i].enemyPrefabs.Length)].gameObject.name + "생성됨");
                EnemyBase enemy = Instantiate(enemies[i].enemyPrefabs[Random.Range(0, enemies[i].enemyPrefabs.Length)]).GetComponent<EnemyBase>();
                enemy.transform.parent = poolRoot.transform;
                enemy.gameObject.SetActive(false);
                enemies[i].pool.AddLast(enemy);
            }
        }
    }

    public EnemyBase GetEnemy(int id)
    {
        if (enemies[id].pool.Count.Equals(0))
        {
            Debug.Log("오브젝트 풀에 오브젝트가 없습니다.");
        }
        enemies[id].activePool.AddLast(enemies[id].pool.First.Value);
        enemies[id].pool.RemoveFirst();
        return enemies[id].activePool.Last.Value;
    }

    public void ReturnEnemy(int id)
    {
        if (enemies[id].activePool.Count.Equals(0))
        {
            return;
        }

        enemies[id].pool.AddLast(enemies[id].activePool.First.Value);
        enemies[id].activePool.RemoveFirst();
        Debug.Log(enemies[id].enemyPrefabs[Random.Range(0, enemies[id].enemyPrefabs.Length)].name + "반환됨");
    }

    public void ReturnEnemy(EnemyBase enemy)
    {
        if (enemy.GetType().ToString() == "IndianBlowgun")
        {
            ReturnEnemy(0);
        }else if (enemy.GetType().ToString() == "Obstacle")
        {
            ReturnEnemy(1);
        }
        else if (enemy.GetType().ToString() == "Smuggler")
        {
            ReturnEnemy(2);
        }
        else if (enemy.GetType().ToString() == "Mole")
        {
            ReturnEnemy(3);
        }
        else if (enemy.GetType().ToString() == "Cow")
        {
            ReturnEnemy(4);
        }


    }


    //public EnemyBase GetIndian()
    //{
    //    if (indianPool.Count.Equals(0))
    //    {
    //        Debug.Log("오브젝트 풀에 Indian 오브젝트가 없습니다.");
    //    }

    //    activeIndians.AddLast(indianPool.First.Value);
    //    indianPool.RemoveFirst();
    //    return indianPool.First.Value;
    //}

}
