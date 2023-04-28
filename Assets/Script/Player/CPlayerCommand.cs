using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class CPlayerCommand : CComponent
{
    public GameObject[] modelObj = new GameObject[3];
    public bool isDooge { get; private set; } = false;
    public bool isSkill4 { get; private set; } = false;
    private bool isDoogeMove = false;
    private bool isSkill2Up = false;
    private bool isSkill4Up = false;
    private bool isSpecialAttackEffectUI = false;

    public Transform specialAttackEffectUI = null;
    private Coroutine isDone = null;
    public GameObject skill4Effect_1;
    public GameObject skill4Effect_2;

    public GameObject skill2Effect;
    public GameObject slashEffect;
    public GameObject windMill;
    public GameObject footStep;
    public GameObject skill5Effect;
    enum EMotion
    {
        eIdle,
        eNormalAttack,
        eSkill1,
        eSkill2,
        eSkill3,
        eSkill4,
        eSkill5,
        eSkill6
    }
    private Animator playerAnimator;
    private int combo;
    private Vector3 vec3Nor = Vector3.zero;
    public bool isNowAction { get; private set; } = false;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        combo = 0;
    }

    private void FixedUpdate()
    {

    }

    void Update()
    {
        if (isSpecialAttackEffectUI == true)
        {
            if (specialAttackEffectUI.position.x >= 128)
            {
                specialAttackEffectUI.Translate(-2000 * Time.unscaledDeltaTime, 0, 0);
            }
            else
            {
                specialAttackEffectUI.Translate(-300 * Time.unscaledDeltaTime, 0, 0);
            }
        }
        if (playerAnimator != null)
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dodge_Front"))
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
                {
                    isDooge = false;
                    Debug.Log("³¡?");
                }
            }
        }

        if (isDooge == false)
        {
            if (isNowAction == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isNowAction = true;
                    StartCoroutine(ComboSystem(EMotion.eNormalAttack));
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    isNowAction = true;
                    playerAnimator.SetTrigger("Skill_4");
                    isDone = StartCoroutine(ComboSystem(EMotion.eSkill4));
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    isNowAction = true;
                    playerAnimator.SetTrigger("Skill_1");
                    isDone = StartCoroutine(ComboSystem(EMotion.eSkill1));
                    var obj = Instantiate(windMill, transform);
                    DestroyObject(obj, 1.5f);
                }
                if (Input.anyKeyDown)
                {
                    Debug.Log("dsaopif");
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    isNowAction = true;
                    playerAnimator.SetTrigger("Skill_5");
                    isDone = StartCoroutine(ComboSystem(EMotion.eSkill5));
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    isNowAction = true;
                    playerAnimator.SetTrigger("Skill_2");
                    isDone = StartCoroutine(ComboSystem(EMotion.eSkill2));
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    isNowAction = true;
                    playerAnimator.SetTrigger("Skill_3");
                    isDone = StartCoroutine(ComboSystem(EMotion.eSkill3));
                }
                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    isNowAction = true;
                    isSpecialAttackEffectUI = true;
                    playerAnimator.SetTrigger("Skill_6");
                    Time.timeScale = 0.01f;
                    isDone = StartCoroutine(ComboSystem(EMotion.eSkill6));
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && isSkill2Up == false && isSkill4Up == false)
            {
                isDooge = true;
                isSkill2Up = false;
                isSkill4Up = false;
                isNowAction = false;
                if (isDone != null)
                {
                    StopCoroutine(isDone);
                }
                playerAnimator.SetTrigger("dooge");
                vec3Nor = transform.forward;
                StartCoroutine("DoogiAnimation");
                Debug.Log(vec3Nor);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }

        }
        if (isSkill2Up == true)
        {
            GetComponentInParent<CPlayerMove>()?.playerController.Move((Vector3.up + transform.forward) * Time.deltaTime * 15);
        }
        if (isSkill4Up == true)
        {
            GetComponentInParent<CPlayerMove>()?.playerController.Move((Vector3.up) * Time.deltaTime * 15);
        }
        if (isDoogeMove == true)
        {
            GetComponentInParent<CPlayerMove>()?.playerController.Move(vec3Nor * Time.deltaTime * 10);
        }

    }

    IEnumerator DoogiAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        isDoogeMove = true;
        yield return new WaitForSeconds(0.5f);
        isDoogeMove = false;
    }
    IEnumerator ComboSystem(EMotion motionNumber)
    {
        switch (motionNumber)
        {
            case EMotion.eSkill2:
                {
                    yield return new WaitForSeconds(0.4f);
                    isSkill2Up = true;
                    yield return new WaitForSeconds(0.3f);
                    isSkill2Up = false;
                    isNowAction = true;
                    yield return new WaitForSeconds(0.7f);
                    var obj = Instantiate(skill2Effect, transform.position, Quaternion.identity);
                    Destroy(obj, 4);
                    obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    obj.transform.localRotation = transform.rotation;
                }
                break;
            case EMotion.eNormalAttack:
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_3Combo_2"))
                {
                    playerAnimator.SetTrigger("normalAttack_3");
                    yield return new WaitForSeconds(0.1f);

                    var sEffect = Instantiate(slashEffect, this.transform);
                    sEffect.transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);
                    sEffect.transform.localPosition = new Vector3(0, 0.1f, 0);
                    Destroy(sEffect, 0.35f);

                    sEffect.transform.localRotation = Quaternion.Euler(0, 0, -46);
                    yield return new WaitForSeconds(1.2f);
                }
                else if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_3Combo_1"))
                {
                    playerAnimator.SetTrigger("normalAttack_2");
                    yield return new WaitForSeconds(0.05f);

                    var sEffect = Instantiate(slashEffect, this.transform);
                    sEffect.transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);

                    sEffect.transform.localPosition = new Vector3(0, 0.1f, 0);
                    Destroy(sEffect, 0.35f);

                    sEffect.transform.localRotation = Quaternion.Euler(0, 0, 90);
                    yield return new WaitForSeconds(0.32f);
                }
                else
                {
                    playerAnimator.SetTrigger("normalAttack_1");
                    yield return new WaitForSeconds(0.2f);
                    var sEffect = Instantiate(slashEffect, this.transform);
                    sEffect.transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);

                    sEffect.transform.localPosition = new Vector3(0, 0.1f, 0);
                    Destroy(sEffect, 0.35f);

                    sEffect.transform.localRotation = Quaternion.Euler(0, 0, 180 - 14f);
                    yield return new WaitForSeconds(0.3f);
                }
                break;
            case EMotion.eSkill1:
                break;
            case EMotion.eSkill3:
                break;
            case EMotion.eSkill4:
                yield return new WaitForSeconds(0.3f);
                var obj2 = Instantiate(skill4Effect_1, transform.position, Quaternion.identity);
                obj2.transform.localScale = new Vector3(0.82f, 0.82f, 0.82f);
                obj2.transform.localRotation = transform.rotation;
                isSkill4Up = true;
                yield return new WaitForSeconds(0.7f);
                isSkill4Up = false;
                isSkill4 = true;
                modelObj[0].SetActive(false);
                modelObj[1].SetActive(false);
                modelObj[2].SetActive(false);
                playerAnimator.speed = 0f;
                var obj3 = Instantiate(skill4Effect_2, transform.position, Quaternion.identity);
                obj3.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                Vector3 dir = transform.localRotation * Vector3.forward;
                obj3.transform.Translate(dir.x, 1, dir.z);

                yield return new WaitForSeconds(2.2f);
                playerAnimator.speed = 1f;

                modelObj[0].SetActive(true);
                modelObj[1].SetActive(true);
                modelObj[2].SetActive(true);
                isSkill4 = false;
                break;
            case EMotion.eSkill5:
                yield return new WaitForSeconds(.8f);
                var obj5 = Instantiate(skill5Effect, transform.position, transform.rotation);
                obj5.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case EMotion.eSkill6:
                break;
        }

        isNowAction = false;
        yield return null;
    }

}