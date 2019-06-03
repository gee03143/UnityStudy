using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : EnemyBase
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        base.OnEnable();
        rb.velocity = new Vector2(0, -data._speed);
    }

    public override void ShowDeathEffect()
    {
    }
}
