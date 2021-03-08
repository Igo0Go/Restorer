using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class FreeCompanion : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private List<Transform> wayPoints;

    private Transform myTransform;
    private bool move;

    private void Start()
    {
        myTransform = transform;
        move = false;
        anim.SetBool("Move", false);
    }

    public void ComeToNext()
    {
        move = true;
        agent.destination = wayPoints[0].position;
        anim.SetBool("Move", move);
    }

    private void Update()
    {
        if(move)
        {
            if (Vector3.Distance(myTransform.position, wayPoints[0].position) < 0.5f)
            {
                move = false;
                myTransform.position = wayPoints[0].position;
                myTransform.rotation = wayPoints[0].rotation;
                anim.SetBool("Move", move);
                wayPoints.RemoveAt(0);
            }
        }
    }
}
