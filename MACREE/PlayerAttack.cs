using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public Transform firePoint;

    public float _damage;               //총알이 입히는 피해입니다. 다른 총기류가 구현될 경우 수정 필요
    public float _fireDelay;            //총알 발사 후 다음 총알을 발사하기까지의 시간입니다.
    public float _timeToShoot;          //다음 총알 발사 가능 시간입니다. 마지막 발사 시간에 딜레이를 더해 구합니다.
    public float _maxAmmo;              //총알의 최대 장전 수입니다
    public float _currentAmmo;          //현재 총알의 장전 수입니다.
    public float _reloadTime;           //총알 장전에 소요되는 시간입니다.

    public float camShakeAmt = 0.02f;     //총 발사 시 카메라 흔들림 수치
    public float camShakeLength = 0.1f; //총 발사 시 카메라 흔들림 길이
    public string reloadSound = "Reload";
    public string shotSound= "Shot";
    public string bombSound = "Bomb";

    //폭탄 관련 변수
    public LayerMask bombTarget;
    public GameObject bombEffect;
    public float _bombRange;
    public float _bombCooldown;
    float nextBomb = 0;
    public float bCamShakeAmt = 0.1f;     //폭탄 카메라 흔들림 수치
    public float bCamShakeLength = 0.5f; //폭탄 카메라 흔들림 길이

    public Image img_NormalAttack;

    CameraShake camshake;
    AudioManager audioManager;


    public void Start()
    {
        _currentAmmo = _maxAmmo;
        _timeToShoot = Time.time;

        //cache
        audioManager = AudioManager.instance;
        camshake = GM.gm.camShake;

        if (audioManager == null)
        {
            Debug.LogError("No AudioManager reference in GM ");
        }

        AttackInputArea.instance.onSwipe += Bomb;
        AttackInputArea.instance.onTouch += Attack;
    }

    public void Attack()
    {
        //공격 딜레이 구현을 위해 분리해둔 함수, 리볼버이므로 탄창 상황, 딜레이에 따라 공격을 안하게 구현해야 함.
        if (_timeToShoot < Time.time)
        {

            Shoot();
            _currentAmmo--;

            _timeToShoot = Time.time + _fireDelay;
            if (_currentAmmo.Equals(0))
            {
                Reload();
            }

            img_NormalAttack.fillAmount = _currentAmmo / _maxAmmo;  //currentAmmo에 따라 총 모양 색을 채운다.

        }
      
        //나중에 딜레이 구현할 예정
    }


    public void Shoot()
    {
        PlayerBullet bullet = PoolManager.instance.GetPlayerBullet();
        bullet.transform.position = firePoint.position;
        bullet.gameObject.SetActive(true);
        bullet.Launch();


        //카메라 흔들림 처리
        camshake.Shake(camShakeAmt, camShakeLength);

        //효과음 출력
        audioManager.PlaySound(shotSound);

    }

    public void Reload()
    {
        Debug.Log("재장전 중!");
        _currentAmmo = _maxAmmo;
        _timeToShoot = Time.time + _reloadTime;

        //효과음 출력
        audioManager.PlaySound(reloadSound);
    }

    public void Bomb()
    {
        if (nextBomb < Time.time)
        {
            CastBomb();
            nextBomb = Time.time + _bombCooldown;
        }
    }

    private void CastBomb()
    {
        Collider2D[] bullets = Physics2D.OverlapCircleAll(transform.position, _bombRange, bombTarget);
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].gameObject.SetActive(false);
        }

        bombEffect.SetActive(true);
        Invoke("SetEffectToFalse", 1);

        //카메라 흔들림 처리
        camshake.Shake(bCamShakeAmt, bCamShakeLength);

        //효과음 출력
        audioManager.PlaySound(bombSound);
    }

    private void SetEffectToFalse()
    {
        bombEffect.SetActive(false);
    }
}
