using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinalScript : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;

    private void Awake()
    {
        GameData.Init();
    }

    public void Final(GameObject obj)
    {
        director.Play();
        obj.SetActive(false);
    }
}
