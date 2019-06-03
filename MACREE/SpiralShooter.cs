using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralShooter : EnemyBase
{
    public Transform firePoint;
    public GameObject bulletObject;

    Vector3 _angle;
    public float _angleRate = 10f;
    public float _shootDelay = 0.3f;
    private float timeToShoot = 0;

    // Start is called before the first frame update
    void Start()
    {
        _angle = firePoint.transform.eulerAngles;
        timeToShoot = _shootDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeToShoot <= 0)
        {
            _angle.z += _angleRate;
            firePoint.transform.eulerAngles = _angle;
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
        GameObject bullet1 = Instantiate(bulletObject, firePoint.position, Quaternion.identity);

        Bullet b1 = bullet1.GetComponent<Bullet>();

        b1.Launch(firePoint.position, firePoint.position + firePoint.transform.right);
    }
}
