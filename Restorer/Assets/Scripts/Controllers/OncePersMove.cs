using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class OncePersMove : MonoBehaviour
{
    [HideInInspector]
    public DanceStyle style;

    [SerializeField, Range(1, 5)]
    private float speed = 1;
    [SerializeField, Range(1, 40)]
    private float jumpForce = 10;
    [SerializeField, Range(1, 100)]
    private float gravity = 60;
    [SerializeField]
    private LayerMask ignoreMask;
    [SerializeField, Range(0, 1)]
    private float jumpRayLength = 0.3f;

    private CharacterController characterController;
    private Animator anim;

    private bool move;
    private float verticalAxis;

    private Vector3 dir;

    void Awake()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        ChangeStyle();
    }

    private void Move()
    {
        if (OnGround())
        {
            float hor = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");

           

            if (Input.GetKeyDown(KeyCode.Space))
            {
                move = false;
                anim.SetTrigger("Jump");
            }

          
            if (vert < 0)
            {
                dir = -transform.forward * speed;
                anim.SetFloat("LeftRight", 0, Time.deltaTime, Time.deltaTime);
                anim.SetFloat("ForwardBack", -1, Time.deltaTime, Time.deltaTime);
                if (move)
                {
                    characterController.Move(dir * Time.deltaTime);
                    return;
                }
            }
            else
            {
                anim.SetFloat("ForwardBack", 0, Time.deltaTime, Time.deltaTime);
            }
            if (hor != 0)
            {
                dir = transform.right * speed * hor;
                anim.SetFloat("LeftRight", hor, Time.deltaTime, Time.deltaTime);
                if (move)
                {
                    characterController.Move(dir * Time.deltaTime);
                    return;
                }
            }
            else
            {
                dir = transform.forward * speed;
                anim.SetFloat("LeftRight", 0, Time.deltaTime, Time.deltaTime);
                if (move)
                {
                    characterController.Move(dir * Time.deltaTime);
                }
            }
        }
        verticalAxis -= gravity;
        Debug.Log(verticalAxis);
        verticalAxis = Mathf.Clamp(verticalAxis, -40, 40);
        characterController.Move((transform.forward * (move ? 1 : 0) + Vector3.up * verticalAxis) * Time.deltaTime);
    }
    private bool OnGround()
    {

        if(Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, jumpRayLength, ~ignoreMask))
        {
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, Vector3.down * jumpRayLength, Color.red, 0.3f);
            Debug.Log(hit.collider.name);
            return true;
        }
        return false;
    }
    private void ChangeStyle()
    {
        if(Input.GetKey(KeyCode.H))
        {
            style = DanceStyle.Strong;
        }
        else if (Input.GetKey(KeyCode.J))
        {
            style = DanceStyle.Middle;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            style = DanceStyle.Fast;
        }
        else
            style = DanceStyle.Default;
        anim.SetInteger("DanceStyle", (int)style);
    }


    public void StartMove() => move = true;
    public void StopMove() => move = false;
    public void JumpAction() => verticalAxis = jumpForce;

    public void RightLegStay() => anim.SetInteger("Leg", 1);
    public void LeftLegStay() => anim.SetInteger("Leg", -1);
    public void BothLegsStay() => anim.SetInteger("Leg", 0);

    private void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Construct"))
        {
            ConstructionPoint point = other.GetComponent<ConstructionPoint>();
            if (point != null && point.style == style)
                point.Construct();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Final"))
        {
            other.GetComponent<FinalScript>().Final(gameObject);
        }
    }
}

public enum DanceStyle
{
    Default = 0,
    Strong = 1,
    Middle = 2,
    Fast = 3
}
