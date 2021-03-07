using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioDispetcher : MonoBehaviour
{
    public float MusicVolume
    {
        get { return _musicVolume; }
        set 
        {
            _musicVolume = value;
            MusicVolumeChanged?.Invoke(_musicVolume);
        }
    }
    private float _musicVolume;
    public float SoundVolume
    {
        get { return _soundVolume; }
        set
        {
            _soundVolume = value;
            SoundsVolumeChanged?.Invoke(_soundVolume);
        }
    }
    private float _soundVolume;
    public float VoiceVolume
    {
        get { return _voiceVolume; }
        set
        {
            _voiceVolume = value;
            VoiceVolumeChanged?.Invoke(_voiceVolume);
        }
    }
    private float _voiceVolume;

    public Action<float> MusicVolumeChanged;
    public Action<float> SoundsVolumeChanged;
    public Action<float> VoiceVolumeChanged;
}
