using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayActor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent action;
    [SerializeField]
    private float delayTime;

    public void Action()
    {
        StartCoroutine(ActionCoroutine());
    }

    private IEnumerator ActionCoroutine()
    {
        yield return new WaitForSeconds(delayTime);
        action?.Invoke();
        Destroy(gameObject);
    }
}
