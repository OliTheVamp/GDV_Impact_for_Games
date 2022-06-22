using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{

    public SFX[] allSounds;

    // Instantiations
    void Awake()
    {
        foreach (SFX thisSFX in allSounds)
        {
            thisSFX.sourceData = gameObject.AddComponent<AudioSource>();
            thisSFX.sourceData.clip = thisSFX.SoundClip;

            thisSFX.sourceData.volume = thisSFX.volume;
        }
    }

    public void PlaySFX (string clipName)
    {

        SFX thissSFX = Array.Find(allSounds, SFX => SFX.SFXName == clipName);

        if (thissSFX == null) return;
        else thissSFX.sourceData.Play();
    }
}