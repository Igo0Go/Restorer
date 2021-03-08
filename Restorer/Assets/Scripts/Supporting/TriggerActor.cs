using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerActor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent action;

    public void InvokeEvent()
    {
        action?.Invoke();
        Destroy(gameObject);
    }
}
