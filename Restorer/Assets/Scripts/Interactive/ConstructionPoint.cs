using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class ConstructionPoint : MonoBehaviour
{
    public DanceStyle style;

    [SerializeField]
    private UnityEvent CompleteEvent;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        //GameData.AddInMax(style);
    }

    public void Construct()
    {
        //GameData.AddInCurrent(style);
        anim.SetTrigger("Construct");
        CompleteEvent?.Invoke();
        Destroy(GetComponent<Collider>());
    }
}
