using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ConstructionPoint : MonoBehaviour
{
    public DanceStyle style;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        GameData.AddInMax(style);
    }

    public void Construct()
    {
        GameData.AddInCurrent(style);
        anim.SetTrigger("Construct");
        Destroy(GetComponent<Collider>());
    }
}
