using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    //캐릭터 이동 관련 변수
    Rigidbody2D rb;                         //주인공 캐릭터의 Rigidbody 컴포넌트
    Vector2 newPosition;                    //터치 입력 정보, 캐릭터 속도, 시간을 통해 계산된 캐릭터의 다음 위치
    public float _characterSpeed = 5f;      //캐릭터의 이동 속도
    public float _playerHealth = 10f;

    float direction;

    //캐릭터 피격 관련 변수
    public float undamagableTime;
    bool damagable = true;
    public float camShakeAmt = 0.04f;     //피격 시 카메라 흔들림 수치
    public float camShakeLength = 0.1f; //피격 시 카메라 흔들림 길이

    //점프 관련 변수
    public float jumpTime;
    bool isJumping = false;

    //사운드 관련 변수
    public string gruntVoiceName = "Grunt";

    CameraShake camshake;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camshake = GM.gm.camShake;
        MovingInputArea.instance.onDoubleTouch += Jump;
    }

    private void Update()
    {
        //매 프레임마다 터치 입력 정보를 가져옵니다.
        GetInput();
    }

    private void FixedUpdate()
    {
        //FixedUpdate에 Move를 처리하지 않으면 캐릭터의 이동 속도가 프레임에 따라 달라지는 현상 발생, 이동 관련 함수는 여기서 처리해주세요
        Move();
    }

    void OnTogglePause(bool active)
    {
        if (active)
        {
            rb.Sleep();
        }
        else
        {
            rb.WakeUp();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!damagable)
            return;

        if (other.tag.Equals("Bullet") || other.tag.Equals("Enemy"))
        {
            Debug.Log("Player Hit on " + other.name);
            other.gameObject.SetActive(false);
            DamagePlayer(); ;
            return;
        }

        if(other.tag.Equals("Obstacle") && !isJumping)
        {
            Debug.Log("Player Hit on " + other.name);
            other.gameObject.SetActive(false);
            DamagePlayer();
            setJumpToFalse();
            return;
        }

        if(_playerHealth.Equals(0))
        {
            GM.gm.EndGame();
        }
    }

    public void ShowHitEffect()
    {
        GameObject effect = PoolManager.instance.GetDeathEffect();
        effect.transform.position = gameObject.transform.position;
        effect.GetComponent<ParticleSystem>().Play();
    }

    public void DamagePlayer()
    {
        _playerHealth--;
        damagable = false;

        //effects
        ShowHitEffect();
        AudioManager.instance.PlaySound(gruntVoiceName);
        camshake.Shake(camShakeAmt, camShakeLength);
        

        StartCoroutine(UnDamagable());
    }

    public void GetInput()
    {
        if (MovingInputArea.instance.GetPosX().Equals(0))
        {
            direction = 0;
            return;
        }
        //터치 입력 정보를 가져오는 함수
        direction = MovingInputArea.instance.GetPosX() - rb.position.x;

    }

    public void Move()
    {
        //캐릭터 이동 처리, 다음 위치를 계산해 캐릭터를 다음 위치로 이동시킵니다.
  
        newPosition.x = Mathf.Clamp(rb.position.x + direction * _characterSpeed * Time.deltaTime, -GM.ScreenWidth * 0.5f, GM.ScreenWidth * 0.5f); //캐릭터가 화면 밖으로 나가지 못하게 만드는 코드
        newPosition.y = rb.position.y;

        rb.MovePosition(newPosition);
    }
    
    public void Jump()
    {
        isJumping = true;
        transform.localScale = new Vector3(2, 2, 2);
        StartCoroutine(JumpTimer());
    }

    IEnumerator JumpTimer()
    {
        while (isJumping)
        {
            Debug.Log("start Coroutine");
            yield return new WaitForSeconds(jumpTime);
            isJumping = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void setJumpToFalse()
    {
        isJumping = false;
        transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator UnDamagable()
    {

        while (!damagable)
        {
            Debug.Log("start Coroutine");
            yield return new WaitForSeconds(undamagableTime);
            damagable = true;
        }

    }


}
