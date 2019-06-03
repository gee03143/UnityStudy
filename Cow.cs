using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : EnemyBase
{
    Rigidbody2D rb;

    public float _skillPointAmount = 10f;//발사 후 다음 공격까지의 딜레이

    private void OnEnable()
    {
        base.OnEnable();
        rb.velocity = new Vector2(0, -data._speed);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    public override void takeDamage()
    {
        data._health--;
        if (data._health.Equals(0))
        {
            GiveSkillPoint();
            gameObject.SetActive(false);
            //PoolManager.instance.ReturnIndian();
        }
    }

    private void GiveSkillPoint()
    {
        Debug.Log("흑우의 기운이 솟아납니다!");
        AudioManager.instance.PlaySound("PowerUp");
    }
}
