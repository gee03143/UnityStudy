using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovingInputArea : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static MovingInputArea instance;

    private Image inputArea;
    private Vector2 touchPos;
    private Camera cam;
    private float lastTouch;

    public float doubleTouchMaxTime = 0.5f;

    public delegate void dTouchCallback();
    public dTouchCallback onDoubleTouch;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        inputArea = GetComponent<Image>();                                      // 터치 입력 범위
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        cam.ScreenToWorldPoint(ped.position);
        touchPos = cam.ScreenToWorldPoint(ped.position);
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        if(Time.time - lastTouch < doubleTouchMaxTime)
        {
            Debug.Log("더블 터치 입력 발생");
            onDoubleTouch.Invoke();
        }
        lastTouch = Time.time;
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        //터치 입력이 끝났을때 조이스틱 이미지와 벡터를 초기값으로 되돌림
        touchPos = Vector3.zero;
    }

    public Vector2 GetTouchPos()
    {
        return touchPos;
    }


    //입력 벡터 값을 가져오는 함수들, 이 값이 필요해지면 사용할것
    public float GetPosX()
    {
        return touchPos.x;
    }

    public float GetPosY()
    {
        return touchPos.y;
    }
}