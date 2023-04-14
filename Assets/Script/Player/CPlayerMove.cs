using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMove : MonoBehaviour
{
    private CharacterController playerController;
    private Animator playerAnimator;
    private Transform cameraTransform;
    private Transform cameraTransformParent;

    public GameObject playerModel;
    private CPlayerCommand playerCommand;

    public float runSpeed = 4f;
    public float rotationSpeed = 360;
    public float gravity = 10.0f;
    public float mouseSensitivity = 2.0f;

    private Vector3 move;
    private Vector3 mouseMove;


    void Start()
    {
        playerController = this.gameObject.GetComponent<CharacterController>();
        playerAnimator = playerModel.gameObject.GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        cameraTransformParent = cameraTransform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        playerCommand = playerModel.gameObject.GetComponent<CPlayerCommand>();
    }


    void Update()
    {
        Balance();
        playerModel.transform.localPosition = new Vector3(0, 0, 0);
        if (playerCommand.isNowAction == false)
        {
            if (playerController.isGrounded)
            {
                GroundChecking();
                MovePlayer(1.0f);
            }
            else
            {
                move.y -= gravity * Time.deltaTime;
                MovePlayer(0.01f);
            }
        }

        playerController.Move(move * Time.deltaTime);
    }

    private void LateUpdate()
    {
        cameraTransformParent.position = transform.position + Vector3.up * 0.6f;

        // 마우스 움직임을 가감
        mouseMove +=
            new Vector3(
                0,
                Input.GetAxisRaw("Mouse X") * mouseSensitivity, 0);

        if (mouseMove.x < -5)
        {
            mouseMove.x = -5;
        }
        else if (50 < mouseMove.x)
        {
            mouseMove.x = 50;
        }

        cameraTransformParent.localEulerAngles = mouseMove;
    }

    void Balance()
    {
        if (transform.eulerAngles.x != 0 || transform.eulerAngles.z != 0)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }

    void MovePlayer(float rate)
    {
        float tempMoveY = move.y;

        move.y = 0;

        Vector3 inputMoveXZ = new Vector3
        (
            -Input.GetAxis("Vertical"),
            0,
            Input.GetAxis("Horizontal")
        );

        //대각선 이동 자체가 루트 2배의 속도를 갖는 것을 막기 위해서 속도가 1 이상 되면 그냥 노말라이즈 시킨다.
        //  -> 속도 곱해서 -> 정규화

        //연산을 두 번 할 필요 없이 따로 저장
        float inputMoveXZMagnitude = inputMoveXZ.sqrMagnitude;

        inputMoveXZ = transform.TransformDirection(inputMoveXZ);

        if (inputMoveXZMagnitude <= 1)
        {
            inputMoveXZ *= runSpeed;
        }
        else
        {
            inputMoveXZ = inputMoveXZ.normalized * runSpeed;
        }

        // 조작 중에만 카메라의 방향에 상대적으로 캐릭터가 움직이도록
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Quaternion cameraRotation = cameraTransformParent.rotation;

            //코스트 절약을 해주고 -> 연산 x
            cameraRotation.x = cameraRotation.z = 0;

            transform.rotation = Quaternion.Slerp
            (
                transform.rotation,
                cameraRotation,
                10.0f * Time.deltaTime
            );

            if (move != Vector3.zero)
            {
                //X
                Quaternion characterRotation = Quaternion.LookRotation(move);

                characterRotation.x = characterRotation.z = 0;

                playerModel.transform.rotation = Quaternion.Slerp
                (
                    playerModel.transform.rotation,
                    characterRotation,
                    10.0f * Time.deltaTime
                );
            }
            //쿼터니언 사용 이유: 코드의 범용성을 위해서(파라미터보다 수정하기 편함)

            //MoveTowards: 관성 함수
            // ㄴ 현재 / 목표 / 속도
            move = Vector3.MoveTowards(move, inputMoveXZ, rate * runSpeed);
        }
        else
        {
            move = Vector3.MoveTowards(move, Vector3.zero, (1 - inputMoveXZMagnitude) * rate * runSpeed);
        }

        float speed = move.sqrMagnitude;
        playerAnimator.SetFloat("speed", speed);

        //연산을 하지 않고 물고 있던 메모리를 수거한다.
        move.y = tempMoveY;
    }

    void GroundChecking()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.5f))
        {
            move.y = -5;
        }
        else
        {
            move.y = -1;
        }
    }
}