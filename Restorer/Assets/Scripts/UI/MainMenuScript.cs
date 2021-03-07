using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
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

    public void OnNewGameClick()
    {
        CloseAllPanel();
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
