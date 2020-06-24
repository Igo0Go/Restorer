using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PairPersController : MonoBehaviour
{
    [HideInInspector]
    public DanceStyle style;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private List<AudioClip> errorClips;
    [SerializeField]
    private Transform cam;
    [SerializeField, Range(0.5f, 5)]
    private float speed = 0.9f;

    private CharacterController characterController;
    private Animator anim;

    private bool move;

    private Vector3 dir;

    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        ChangeStyle();
    }

    private void LateUpdate()
    {
        cam.LookAt(transform.position + Vector3.up);
    }

    private void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 camForward = cam.forward;
        camForward.y = 0;

        dir = (camForward * ver + cam.right * hor).normalized;

        anim.SetFloat("Move", dir.magnitude, Time.deltaTime * 5, Time.deltaTime);

        if (dir.magnitude != 0)
        {
            transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 5);
            characterController.Move(((dir * speed) - Vector3.up * 10) * Time.deltaTime);
        }  
    }
  
    private void ChangeStyle()
    {
        if (Input.GetKey(KeyCode.H))
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Pair"))
        {
            if(!source.isPlaying)
            {
                source.PlayOneShot(errorClips[Random.Range(0, errorClips.Count)]);
            }
        }
    }
}
