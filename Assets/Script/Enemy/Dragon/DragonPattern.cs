using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPattern : MonoBehaviour
{
    private bool isOncePlayerMeet = false;
    private GameObject playerObject = null;
    private Animator dragonAnimator = null;
    public Transform dragonModel;
    private bool isStart = false;

    enum EAnimationState
    {
        eIdle,
        eTakeOff,
    };
    void Start()
    {
        dragonAnimator = GetComponentInChildren<Animator>();
        playerObject = GameObject.Find("Player");
    }

    void Update()
    {
        if (isOncePlayerMeet == false)
        {
            if (Vector3.Distance(playerObject.transform.position, this.transform.position) < 5)
            {
                dragonAnimator.SetTrigger("StandUp");
                isOncePlayerMeet = true;
                StartCoroutine(AnimationCo(EAnimationState.eTakeOff));
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {

        }
    }

    private void LateUpdate()
    {
        dragonModel.transform.localPosition = new Vector3(0, 0, 0);
    }

    IEnumerator AnimationCo(EAnimationState state)
    {
        switch (state)
        {
            case EAnimationState.eTakeOff:
                yield return new WaitForSeconds(1f);
                break;
        }
    }
}
