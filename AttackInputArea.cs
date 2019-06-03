using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackInputArea : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{

    public static AttackInputArea instance;
    
    Vector2 touchStartPos;  // 화면에 터치된 위치 벡터입니다.
    Vector2 touchEndPos;
    private float holdMinValue = 1f;         //홀드가 구현될 경우 홀드로 인정되는 시간의 최소값입니다.
    float touchStartTime;
    float swipeMinDist = 30f;
    float swipeXDelta;


    public delegate void swpieCallback();
    public swpieCallback onSwipe;

    public delegate void touchCallback();
    public touchCallback onTouch;


    //쿨타임 코드 이식
    public Image img_Skill_1;
    public Image img_Skill_2;
    public float Skill1_cooltime;
    public float Skill2_cooltime;
    public bool SkillOn_1;
    public bool SkillOn_2;

    // Use this for initialization
    void Start()
    {
        SkillOn_1 = true;
        SkillOn_2 = true;
    }

    
    public void Skill_1_Use()
    {
        if (SkillOn_1)
        {
            SkillOn_1 = false;
            StartCoroutine(CoolTime1(Skill1_cooltime));
        }

    }

    public void Skill_2_Use()
    {
        if (SkillOn_2)
        {
            SkillOn_2 = false;
            StartCoroutine(CoolTime2(Skill2_cooltime));
        }
    }

    IEnumerator CoolTime1(float cool)
    {
        float cooltime = 0f;
        while (cooltime < 1.0f)
        {
            cooltime += Time.deltaTime / cool;
            img_Skill_1.fillAmount = cooltime;
            yield return new WaitForFixedUpdate();
        }
        SkillOn_1 = true;

    }
    IEnumerator CoolTime2(float cool)
    {
        float cooltime = 0f;
        while (cooltime < 1.0f)
        {
            cooltime += Time.deltaTime / cool;
            img_Skill_2.fillAmount = cooltime;
            yield return new WaitForFixedUpdate();
        }
        SkillOn_2 = true;

    }

    // 쿨 타임 코드 이식

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {

        //AttackInputArea에 터치 입력 들어옴
        touchStartPos = ped.position;
        touchStartTime = Time.time;

    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        //나중에 필요해질 경우 사용합니다.
        //화면에서 손을 뗄 때 호출됩니다.

        touchEndPos = ped.position;


        SendInput();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        //드래그 중일 때 필요한 동작이 있다면 여기에 정의합니다.
    }

    public void SendInput()
    {
        //터치 입력 판정
        if (Time.time - touchStartTime > holdMinValue)
        {
            Debug.Log("홀드 입력 감지됨");
        }
        else if (Mathf.Abs(touchEndPos.x - touchStartPos.x) > swipeMinDist)
        {
            Debug.Log("스와이프 입력 감지됨");
            Skill_1_Use();
            //playerAttack.Bomb();
            onSwipe.Invoke();
        }
        else
        {
            onTouch.Invoke();  //공격 함수를 실행합니다
        }
    }

    //나중에 집에 가서 테스트해볼 코드
    //    void Update()
    //    {
    //        //플랫폼에 따라 다른 함수를 동작시킴
    //#if UNITY_ANDROID
    //        MobileInput();
    //#endif
    //#if UNITY_STANDALONE_WIN
    //        MouseInput();
    //#endif
    //    }

    //    //pc에서 디버그하기 위한 코드
    //    void MouseInput()
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            touchStartPos = Input.mousePosition;
    //            touchStartTime = Time.time;
    //            return;
    //        }

    //        if (Input.GetMouseButtonUp(0))
    //        {
    //            touchEndPos = Input.mousePosition;
    //            swipeYDelta = touchEndPos.y - touchStartPos.y;
    //            if (swipeYDelta > swipeMinDist)
    //            {
    //                //위쪽 스와이프 감지됨
    //                Debug.Log("위쪽 스와이프 감지됨");
    //            }
    //            else if (swipeYDelta < -swipeMinDist)
    //            {
    //                //아래쪽 스와이프 감지됨
    //                Debug.Log("아래쪽 스와이프 감지됨");
    //            }
    //            else
    //            {
    //                //터치 입력 감지됨
    //                Debug.Log("터치 입력 감지됨");
    //                playerAttack.Attack();
    //            }
    //            return;
    //        }

    //        if (Input.GetMouseButton(0))
    //        {
    //            if (touchStartTime < Time.time - holdMinValue)
    //            {
    //                Debug.Log("홀드 입력 감지됨");
    //            }
    //            return;
    //        }


    //    }

    //    //안드로이드에서 동작 확인함
    //    void MobileInput()
    //    {
    //        if (Input.touchCount > 0)
    //        {
    //            Touch touch = Input.GetTouch(0);

    //            switch (touch.phase)
    //            {
    //                case TouchPhase.Began:
    //                    touchStartPos = touch.position;
    //                    touchStartTime = Time.time;
    //                    break;
    //                case TouchPhase.Stationary:
    //                    if (touchStartTime < Time.time - holdMinValue)
    //                    {
    //                        Debug.Log("홀드 입력 감지됨");
    //                    }
    //                    break;
    //                case TouchPhase.Ended:
    //                    touchEndPos = touch.position;
    //                    swipeYDelta = touchEndPos.y - touchStartPos.y;

    //                    if (swipeYDelta > swipeMinDist)
    //                    {
    //                        //위쪽 스와이프 감지됨
    //                        Debug.Log("위쪽 스와이프 감지됨");
    //                    }
    //                    else if (swipeYDelta < -swipeMinDist)
    //                    {
    //                        //아래쪽 스와이프 감지됨
    //                        Debug.Log("아래쪽 스와이프 감지됨");
    //                    }
    //                    else
    //                    {
    //                        //터치 입력 감지됨
    //                        Debug.Log("터치 입력 감지됨");
    //                        playerAttack.Attack();
    //                    }
    //                    break;
    //            }
    //        }
    //    }

}
