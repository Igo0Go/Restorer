using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioDispetcher : MonoBehaviour
{
    private void OnDestroy()
    {
        AudioData.MusicVolumeChanged = AudioData.SoundsVolumeChanged = AudioData.VoiceVolumeChanged = null;
    }
}

public static class AudioData
{
    public static float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            MusicVolumeChanged?.Invoke(_musicVolume);
        }
    }
    private static float _musicVolume;
    public static float SoundVolume
    {
        get { return _soundVolume; }
        set
        {
            _soundVolume = value;
            SoundsVolumeChanged?.Invoke(_soundVolume);
        }
    }
    private static float _soundVolume;
    public static float VoiceVolume
    {
        get { return _voiceVolume; }
        set
        {
            _voiceVolume = value;
            VoiceVolumeChanged?.Invoke(_voiceVolume);
        }
    }
    private static float _voiceVolume;

    public static Action<float> MusicVolumeChanged;
    public static Action<float> SoundsVolumeChanged;
    public static Action<float> VoiceVolumeChanged;
}
