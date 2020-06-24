using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private AudioSource source;
    
    [Space(10)]
    public bool playMusic;

    void Start()
    {
        if (pausePanel.activeSelf)
            PauseToggle();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
    }

    public void PauseToggle()
    {
        if(pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            if (playMusic)
                source.Play();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            source.Pause();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
