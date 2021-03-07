using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LearnTracker : MonoBehaviour
{
    [SerializeField]
    private List<KeyCode> buttons;
    [SerializeField]
    private UnityEvent completeEvent;

    private void Update()
    {
        foreach (var item in buttons)
        {
            if(Input.GetKeyDown(item))
            {
                completeEvent?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
