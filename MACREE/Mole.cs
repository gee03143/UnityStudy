using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : EnemyBase
{
    public Transform firePoint;
    Rigidbody2D rb;
    public Vector3 playerPosition;
    public GameObject obstacleObject;

    public float _shootDelay = 2f;//발사 후 다음 공격까지의 딜레이
    private float timeToShoot = 0;

    private void OnEnable()
    {
        base.OnEnable();
        rb.velocity = new Vector2(0, -data._speed);
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timeToShoot = _shootDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeToShoot <= 0)
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            Fire();
            timeToShoot = _shootDelay;
        }
        else
        {
            timeToShoot -= Time.deltaTime;
        }
    }

    void Fire()
    {
        Bullet b1 = PoolManager.instance.GetEmenyBullet();
        b1.transform.position = firePoint.position;
        b1.gameObject.SetActive(true);

        b1.Launch(firePoint.position, playerPosition);
    }

    public override void takeDamage()
    {
        data._health--;
        if (data._health.Equals(0))
        {
            MakeObstacle();
            gameObject.SetActive(false);
            SetToDefault();
            //PoolManager.instance.ReturnIndian();
        }
    }

    private void SetToDefault()
    {
        data._health = 1;
    }

    private void MakeObstacle()
    {
        Vector2 spawnPos = gameObject.transform.position;
        EnemyBase obstacle = PoolManager.instance.GetEnemy(1);
        obstacle.transform.position = spawnPos;
        obstacle.GetComponent<Obstacle>().data._speed = this.data._speed;
        obstacle.gameObject.SetActive(true);
    }

}
