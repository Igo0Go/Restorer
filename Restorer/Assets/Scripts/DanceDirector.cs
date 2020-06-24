using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class DanceDirector : MonoBehaviour
{
    [SerializeField]
    private PairPersController pair;
    [SerializeField]
    private CameraSystem cam;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private List<DanceStage> stages;
    [SerializeField]
    private Light targetDancePoint;
    [SerializeField]
    private List<Color> danceStylesColors;
    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private GameObject firework;

    [SerializeField]
    private List<AudioClip> errorClips;



    [Space(20), SerializeField]
    private bool debug;

    private AudioSource source;
    private AsyncOperation async;
    private Vector3 currentTarget;
    private int currentNumber;
    private float timer;
    private bool opportunityToSay;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        currentNumber = 0;
        timer = 0;
        opportunityToSay = false;
    }

    private void Update()
    {
        CheckDanceStage();
        MoveToTarget();
    }
        
    private void SetStage()
    {
        opportunityToSay = false;
        if(currentNumber < stages.Count-1) 
            Invoke("ReturnOpportunityToSay", 1.5f);
        currentTarget = stages[currentNumber].point.position;
        targetDancePoint.color = danceStylesColors[(int)stages[currentNumber].style];
        currentNumber++;
        if (currentNumber > stages.Count - 1)
        {
            opportunityToSay = false;
            currentNumber = -1;
            firework.SetActive(true);
            director.Play();
            Invoke("LoadNextScene", 7);
        }
    }
    private void MoveToTarget()
    {
        Vector3 dir = currentTarget - transform.position;
        
        if(dir.magnitude > Time.deltaTime * 2)
        {
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
        else
        {
            transform.position = currentTarget;
        }
    }
    private void CheckDanceStage()
    {
        if (currentNumber >= 0)
        {
            timer += Time.deltaTime;
            if (timer > stages[currentNumber].beginTime)
            {
                SetStage();
            }
        }
        if(opportunityToSay)
        {
            if(pair.style != stages[currentNumber-1].style)
            {
                if(!source.isPlaying)
                {
                    source.PlayOneShot(errorClips[Random.Range(0, errorClips.Count)]);
                    opportunityToSay = false;
                }
            }
        }
    }
    private void LoadNextScene() => SceneManager.LoadScene(1);
    private void ReturnOpportunityToSay() => opportunityToSay = true;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (debug)
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
