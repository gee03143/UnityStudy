using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Start is called before the first frame update
    protected void OnEnable()
    {
        StartCoroutine(BoundaryCheck());
    }

    public virtual void Launch(Vector3 startPosition, Vector3 playerPosition)
    {
        Debug.Log("적 공격 발사, 기본 Bellet 오브젝트에서 호출됨");
    }

    private void OnDisable()
    {
        PoolManager.instance.ReturnEnemyBullet();
    }

    IEnumerator BoundaryCheck()
    {
        while (true)
        {
            if (transform.position.y < -GM.ScreenHeight*0.5f)
            {
                gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
