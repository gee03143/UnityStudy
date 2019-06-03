using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{

    public Image img_Skill_1;
    public Image img_Skill_2;
    private float Skill1_cooltime;
    private float Skill2_cooltime;
    public bool SkillOn_1;
    public bool SkillOn_2;

    // Use this for initialization
    void Start()
    {
        SkillOn_1 = true;
        SkillOn_2 = true;
        Skill1_cooltime = GetComponent<PlayerAttack>()._bombCooldown;
        AttackInputArea.instance.onSwipe += Skill_1_Use;        //swpie 동작에 Skill_1_Use를 호출합니다.
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.T)&&SkillOn_1)
        {
            SkillOn_1 = false;
            StartCoroutine(CoolTime1(Skill1_cooltime));
        }
        if (Input.GetKeyDown(KeyCode.R) && SkillOn_2)
        {
            SkillOn_2 = false;
            StartCoroutine(CoolTime2(Skill2_cooltime));
        }*/
    }

    public void Skill_1_Use(){
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
            cooltime += Time.deltaTime/cool;
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
}
