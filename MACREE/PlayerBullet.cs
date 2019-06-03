using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float _bulletSpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        PoolManager.instance.ReturnPlayerBullet();
    }


    private void FixedUpdate()
    {
        Vector2 nextPos = new Vector2(rb.position.x, rb.position.y + _bulletSpeed *Time.deltaTime);

        rb.MovePosition(nextPos);

        if (transform.position.y > GM.ScreenHeight * 0.5f)
        {
            gameObject.SetActive(false);
        }
    }

    public void Launch()
    {
        Debug.Log("발사!");
        //normalized 를 하지 않으면 벡터의 크기에 따라 총알의 속도가 바뀔 수 있음
    }
}
