using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CPlayerCommand : MonoBehaviour
{
    public GameObject slashEffect;

    enum EMotion
    {
        eIdle,
        eNormalAttack,
    }

    private Animator playerAnimator;
    private int combo;
    public bool isNowAction { get; private set; } = false;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        combo = 0;
    }


    void Update()
    {
        if (isNowAction == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isNowAction = true;
                StartCoroutine(ComboSystem(EMotion.eNormalAttack));
                playerAnimator.SetTrigger("normalAttack_1");
            }
        }
    }

    IEnumerator ComboSystem(EMotion motionNumber)
    {
        var sEffect = Instantiate(slashEffect, this.transform);
        sEffect.transform.localScale = new Vector3(0.54f, 0.54f, 0.54f);
        sEffect.transform.localPosition = new Vector3(0, 0.1f, 0);
        Destroy(sEffect, 0.35f);
        switch (motionNumber)
        {
            case EMotion.eNormalAttack:
                switch (combo)
                {
                    case 0:
                        sEffect.transform.localRotation = Quaternion.Euler(0, 0, 180);
                        yield return new WaitForSeconds(0.2f);
                        combo++;
                        break;
                    case 1:
                        sEffect.transform.localRotation = Quaternion.Euler(0, 0, 90);
                        yield return new WaitForSeconds(0.12f);
                        combo++;
                        break;
                    case 2:
                        yield return new WaitForSeconds(1.2f);
                        combo = 0;
                        break;
                }

                isNowAction = false;
                break;
        }

        yield return null;
    }
}