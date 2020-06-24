using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PairAI : MonoBehaviour
{
    [SerializeField]
    private Text debugText;
    [SerializeField]
    private List<DanceStage> stages;

    [Space(20), SerializeField]
    private bool debug;

    private NavMeshAgent agent;
    private Animator anim;

    private int currentNumber;
    private float timer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentNumber = 0;
        timer = 0;
        debugText.text = string.Empty;
    }

    private void Update()
    {
        CheckDanceStage();
        DebugTimerDrawer();
    }

    private void SetStage()
    {
        agent.destination = stages[currentNumber].point.position;
        anim.SetInteger("DanceStyle", (int)stages[currentNumber].style);
        currentNumber++;
        if (currentNumber > stages.Count - 1)
            currentNumber = -1;
    }
    private void CheckDanceStage()
    {
        if(currentNumber >= 0)
        {
            timer += Time.deltaTime;
            if(timer > stages[currentNumber].beginTime)
            {
                SetStage();
            }
        }
        else
        {
            anim.SetInteger("DanceStyle", 0);
            agent.destination = transform.position;
            agent.isStopped = true;
        }

        if(agent.destination != null)
        {
            anim.SetFloat("Move", Vector3.Distance(agent.destination, transform.position) > 0.3f ? 1 : 0, Time.deltaTime * 5, Time.deltaTime);
        }
    }
    private void DebugTimerDrawer()
    {
        if(debug)
        {
            debugText.text = timer.ToString()+ " " + currentNumber;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(debug)
        {
            for (int i = 0; i < stages.Count; i++)
            {
                Transform bufer = stages[i].point;
                if (bufer != null)
                {
                    Color stageColor;
                    switch (stages[i].style)
                    {
                        case DanceStyle.Default:
                            stageColor = Color.green;
                            break;
                        case DanceStyle.Strong:
                            stageColor = Color.red;
                            break;
                        case DanceStyle.Middle:
                            stageColor = Color.cyan;
                            break;
                        case DanceStyle.Fast:
                            stageColor = Color.yellow;
                            break;
                        default:
                            stageColor = Color.green;
                            break;
                    }
                    Gizmos.color = stageColor;
                    Gizmos.DrawSphere(bufer.position + Vector3.up, 0.3f);

                    if (i < stages.Count - 1)
                    {
                        Gizmos.color = Color.black;
                        Gizmos.DrawLine(bufer.position, stages[i + 1].point.position);
                    }
                }
            }
        }
    }
#endif
}

[System.Serializable]
public class DanceStage
{
    [Range(0, 300)]
    public float beginTime;
    public Transform point;
    public DanceStyle style;
}