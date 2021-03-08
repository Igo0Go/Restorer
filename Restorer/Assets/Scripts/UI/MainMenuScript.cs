using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject instructionsPanel;
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private AudioDispetcher audioDispetcher;

    [SerializeField]
    private PlayableDirector startPlayable;
    [SerializeField]
    private LearnScript learnScript;

    [SerializeField]
    private Slider voices;
    [SerializeField]
    private Slider music;
    [SerializeField]
    private Slider sounds;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        music.value = 0.5f;
        sounds.value = 0.7f;
        voices.value = 1f;
    }

    public void OnNewGameClick()
    {
        CloseAllPanel();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        learnScript.NextTip(20);
        learnScript.InvokeOnStartActionAfterTime(21);
        startPlayable.Play();
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

    public void OnExitClick()
    {
        CloseAllPanel();
        Application.Quit();
    }

    private void CloseAllPanel()
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
        AudioData.MusicVolume = music.value;
    }
    public void OnChangeSound()
    {
        AudioData.SoundVolume = sounds.value;
    }
    public void OnChangeVoices()
    {
        AudioData.VoiceVolume = voices.value;
    }
}
