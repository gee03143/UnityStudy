using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smuggler : EnemyBase
{
    public Transform firePoint;
    Rigidbody2D rb;
    public Vector3 playerPosition;

    public float _shootDelay = 2f;//발사 후 다음 공격까지의 딜레이
    public float _maxFireAtOnce = 2f;  //최대 2점사이으로 2의 값 입력
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
            StartCoroutine(Shoot());
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
            gameObject.SetActive(false);
        }
    }

    IEnumerator Shoot()
    {
        int shootCnt = (int)Random.Range(0, _maxFireAtOnce) + 1;    //1발 또는 2발 발사
        for (int i = 0; i < shootCnt; i++)
        {
            Fire();
            yield return new WaitForSeconds(0.1f);
        }


        yield break;
    }
}
