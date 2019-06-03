using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrolling : MonoBehaviour
{
    protected Rigidbody2D rb;

    public float _scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -_scrollSpeed);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.position.y < -GM.ScreenHeight)
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        transform.Translate(0,GM.ScreenHeight * 2, 0);
    }
}
