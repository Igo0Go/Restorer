using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class FreeController : MonoBehaviour
{
    [SerializeField]
    private Transform playerCamera;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private CharacterController controller;

    [SerializeField, Range(1,10)]
    private float moveSpeed = 1;
    [SerializeField, Range(1, 10)]
    private float fallAnimStartThreshold = 3;



    private Transform myTransform;
    private float animDepth = 0.1f;
    private float maxVerSpeed = 40;
    private float vertSpeed;

    void Start()
    {
        myTransform = transform;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float ver, hor;

        ver = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");

        Vector3 camForward = playerCamera.forward;
        Vector3 camRight = playerCamera.right;
        camRight.y = camForward.y = 0;

        Vector3 dir = (camForward * ver + camRight * hor).normalized;

        float move = Mathf.Clamp01(Mathf.Abs(ver) + Mathf.Abs(hor));
        if(move > 0.1f)
        {
            myTransform.forward = dir;
            anim.SetFloat("Locomotion", move, animDepth, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Locomotion", 0, 0.1f, Time.deltaTime);
        }

        if (controller.isGrounded)
        {
            vertSpeed = 0;
        }
        else
        {
            vertSpeed += 9.8f * Time.deltaTime;
        }

        anim.SetBool("InAir", vertSpeed > fallAnimStartThreshold);

        dir += Vector3.down * Mathf.Clamp(vertSpeed, 0, maxVerSpeed);
        controller.Move(dir * moveSpeed * Time.deltaTime);
    }
}
