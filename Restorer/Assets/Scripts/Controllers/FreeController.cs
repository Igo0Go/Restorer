using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FreeController : MonoBehaviour
{
    [SerializeField]
    private Transform playerCamera;

    [SerializeField]
    private Animator anim;

    [SerializeField, Range(1,10)]
    private float moveSpeed = 1;

    private Transform myTransform;
    private float animDepth = 0.1f;



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

        myTransform.position += dir * moveSpeed * Time.deltaTime;

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
    }
}
