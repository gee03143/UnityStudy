using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingBullet : Bullet
{
    public float _bulletSpeed;
    Vector2 target;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        base.OnEnable();
    }

    public override void Launch(Vector3 startPosition, Vector3 targetPosition)
    {
        target = new Vector2(targetPosition.x - startPosition.x, -Mathf.Abs(targetPosition.y - startPosition.y)).normalized;

        //normalized 를 하지 않으면 벡터의 크기에 따라 총알의 속도가 바뀔 수 있음
        //rb.AddForce(target.normalized * _bulletSpeed, ForceMode2D.Impulse);
        rb.velocity = new Vector2(target.x * _bulletSpeed, target.y * _bulletSpeed);
    }
}
