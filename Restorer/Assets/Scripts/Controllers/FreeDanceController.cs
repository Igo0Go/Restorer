using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FreeDanceController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private DanceStyle style;

    [SerializeField]
    private KeyCode fastStyleButton = KeyCode.H;
    [SerializeField]
    private KeyCode middleStyleButton = KeyCode.J;
    [SerializeField]
    private KeyCode strongStyleButton = KeyCode.K;

    private void Update()
    {
        CheckType();
    }

    public void CheckType()
    {
        if (Input.GetKey(strongStyleButton))
        {
            style = DanceStyle.Strong;
        }
        else if (Input.GetKey(middleStyleButton))
        {
            style = DanceStyle.Middle;
        }
        else if (Input.GetKey(fastStyleButton))
        {
            style = DanceStyle.Fast;
        }
        else
        {
            style = DanceStyle.Default;
        }
        anim.SetInteger("Style", (int)style);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Construct"))
        {
            ConstructionPoint point = other.GetComponent<ConstructionPoint>();
            if (point != null && point.style == style)
                point.Construct();
        }
    }
}

public enum DanceStyle
{
    Default = 0,
    Fast = 1,
    Strong = 3,
    Middle = 2
}
