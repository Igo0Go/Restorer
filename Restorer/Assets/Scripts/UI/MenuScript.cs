using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject instructionsPanel;
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private AudioDispetcher audioDispetcher;

    [SerializeField]
    private Slider voices;
    [SerializeField]
    private Slider music;
    [SerializeField]
    private Slider sounds;

    void Start()
    {
        if (pausePanel.activeSelf)
            PauseToggle();

        music.value = 0.5f;
        sounds.value = 0.7f;
        voices.value = 1f;
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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OnInstructionsClick()
    {
        SetPanel(settingsPanel, false);
        SetPanel(instructionsPanel, !instructionsPanel.activeSelf);
    }

    public void OnSettingsClick()
    {
        SetPanel(instructionsPanel, false);
        SetPanel(settingsPanel, !settingsPanel.activeSelf);
    }

    public void CloseAllPanel()
    {
        SetPanel(instructionsPanel, false);
        SetPanel(settingsPanel, false);
    }

    private void SetPanel(GameObject panel, bool value)
    {
        panel.SetActive(value);
    }

    public void OnChangeMusic()
    {
        audioDispetcher.MusicVolume = music.value;
    }
    public void OnChangeSound()
    {
        audioDispetcher.SoundVolume = sounds.value;
    }
    public void OnChangeVoices()
    {
        audioDispetcher.VoiceVolume = voices.value;
    }
}
