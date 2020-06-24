using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslaterSkript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform target;

    public bool move;

    void Update()
    {
        if(move)
        {
            Vector3 dir = target.position - transform.position;
            if(dir.magnitude > Time.deltaTime * 3)
            {
                transform.position += dir * speed * Time.deltaTime;
            }
            else
            {
                transform.position = target.position;
            }
        }
    }
}
