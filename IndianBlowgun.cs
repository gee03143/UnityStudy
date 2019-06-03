using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndianBlowgun : EnemyBase
{
    public Transform firePoint;
    Rigidbody2D rb;

    public float _shootDelay = 1f;//발사 후 다음 공격까지의 딜레이
    public float _fireRate = 2f;    //초당 발사 수
    public float _fireAtOnce = 2f;  //2점사이으로 2의 값 입력
    private float timeToShoot = 0;

    private void OnEnable()
    {
        base.OnEnable();
        rb.velocity = new Vector2(0, -data._speed);
    }

    private void Awake()
    {
        timeToShoot = _shootDelay;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeToShoot <= 0)
        {
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
        b1.Launch(transform.position, transform.position - transform.up);
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
        for (int i = 0; i < _fireAtOnce; i++)         //2점사 블로우건
        {
            Fire();
            yield return new WaitForSeconds(0.1f);
        }


        yield break;
    }
}
