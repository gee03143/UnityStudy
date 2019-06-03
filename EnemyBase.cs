using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public float camShakeAmt = 0.05f;     //사망시 카메라 흔들림 수치
    public float camShakeLength = 0.1f; // 사망시 카메라 흔들림 길이
    public int _score = 100;
    public float _maxHealth;
    public float _health;
    public float _speed;
}

public class EnemyBase : MonoBehaviour
{
    public EnemyData data;

    protected void OnEnable()
    {
        data._health = data._maxHealth;
        StartCoroutine(BoundaryCheck());
    }

    protected void OnDisable()
    {
        PoolManager.instance.ReturnEnemy(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("PlayerBullet"))
        {
            ShowDeathEffect();
            other.gameObject.SetActive(false);
            takeDamage();
        }

    }

    public virtual void ShowDeathEffect()
    {
        GameObject effect = PoolManager.instance.GetDeathEffect();
        effect.transform.position = gameObject.transform.position;
        effect.GetComponent<ParticleSystem>().Play();

        GM.gm.camShake.Shake(data.camShakeAmt, data.camShakeLength);
        GM.gm.GiveScore(100);
    }

    public virtual void takeDamage()
    {
        Debug.Log("Enemy가 피해를 입었습니다. EnemyBase에서 호출");
    }

    IEnumerator BoundaryCheck()
    {
        while (true)
        {
            if (transform.position.y < -GM.ScreenHeight * 0.5f)
            {
                gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
