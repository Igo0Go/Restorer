using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVolumeChanger : MonoBehaviour
{
    public event Action<AudioSource, float> MusicVolumeChanged;

    public AudioType type;
    [HideInInspector] public float maxValue = 1f;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        try
        {
            AudioDispetcher audioCenter = GameObject.FindGameObjectWithTag("AudioCenter").GetComponent<AudioDispetcher>();
            switch (type)
            {
                case AudioType.music:
                    AudioData.MusicVolumeChanged += SetValue;
                    SetValue(AudioData.MusicVolume);
                    break;
                case AudioType.sounds:
                    AudioData.SoundsVolumeChanged += SetValue;
                    SetValue(AudioData.SoundVolume);
                    break;
                case AudioType.voice:
                    AudioData.VoiceVolumeChanged += SetValue;
                    SetValue(AudioData.VoiceVolume);
                    break;
                default:
                    AudioData.MusicVolumeChanged += SetValue;
                    break;
            }
        }
        catch(NullReferenceException)
        {
            Debug.LogError("Объект с настройками громкости звука не найден");
        }
        
    }

    public void SetValue(float newWalue)
    {
        maxValue = newWalue;

        switch (type)
        {
            case AudioType.music:
                MusicVolumeChanged?.Invoke(source, maxValue);
                break;
            case AudioType.sounds:
            source.volume = maxValue;
                break;
            case AudioType.voice:
                source.volume = maxValue;
                break;
            default:
                source.volume = maxValue;
                break;
        }
    }
}

public enum AudioType
{
    music,
    sounds,
    voice
}


//это ПРИМЕР класса, в котором меняются настройки звука. Чаще всего события вызываются из слайдеров в настройках
public class AudioCenter : MonoBehaviour
{
    public float MusicVolume { get; set; } //можно ссылать на слайдеры или значения playerPrefs
    public float SoundVolume { get; set; }

    public Action<float> MusicVolumeChanged;
    public Action<float> SoundsVolumeChanged;
}

