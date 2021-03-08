using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadDispetcher : MonoBehaviour
{
    [SerializeField]
    private Image blackPanel;
    [SerializeField]
    private int sceneIndex;

    private Color bufer = new Color(0, 0, 0, 0);


    private void Start()
    {
        blackPanel.color = Color.black;
        StartCoroutine(StartGameCoroutine());
    }

    public void LoadScene(int index)
    {
        sceneIndex = index;
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        float t = 0;
        while (t < 1)
        {
            blackPanel.color = Color.Lerp(bufer, Color.black, t);
            t += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator StartGameCoroutine()
    {
        float t = 0;
        while (t < 1)
        {
            blackPanel.color = Color.Lerp(Color.black, bufer, t);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
