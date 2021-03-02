using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FreeDanceController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private KeyCode fastStyleButton = KeyCode.H;

    private void Update()
    {
        CheckType();
    }

    public void CheckType()
    {
        if(Input.GetKey(fastStyleButton))
        {
            anim.SetInteger("Style", 1);
            return;
        }

        anim.SetInteger("Style", 0);
    }

}
