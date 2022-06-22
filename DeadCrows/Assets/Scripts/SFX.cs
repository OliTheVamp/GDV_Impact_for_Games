using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SFX
{
    public string SFXName;

    public AudioClip SoundClip;

    [Range(0.0f, 1.0f)]
    public float volume;

    [HideInInspector]
    public AudioSource sourceData;
}
