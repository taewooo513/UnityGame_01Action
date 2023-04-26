using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishmanController : CComponent
{
    private Animator animator = null;
    private GameObject playerObject = null;
    private Transform modelObject;
    private Vector3 move;

    private bool isAttack = false;
    public float speed = 1;

    void Start()
    {
        modelObject = transform.GetChild(0);
        playerObject = GameObject.FindGameObjectWithTag("Player");
        animator = this.transform.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject != null)
        {
            float dist = Vector3.Distance(playerObject.transform.position, this.transform.position);
            Vector3 enemyOfPlayerDirection = playerObject.transform.position - this.transform.position;
            enemyOfPlayerDirection = enemyOfPlayerDirection.normalized;
            move = Vector3.MoveTowards(move, enemyOfPlayerDirection, speed);
            float v = move.sqrMagnitude;
            if (isAttack == false)
            {
                if (dist < 5 && dist >= 1)
                {
                    Quaternion characterRotation = Quaternion.LookRotation(move);
                    characterRotation.x = characterRotation.z = 0;
                    modelObject.transform.localRotation = characterRotation;
                    animator.SetFloat("walkSpeed", v);
                    transform.Translate(move * speed * Time.deltaTime);
                }
                else if (dist < 1)
                {
                    animator.SetFloat("walkSpeed", 0);
                    animator.SetTrigger("attack_1");
                }
                else
                {
                    animator.SetFloat("walkSpeed", 0);
                }
            }
        }
        modelObject.localPosition = new Vector3(0, 0, 0);
    }
}
