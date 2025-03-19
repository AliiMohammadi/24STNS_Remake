using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SoundPlayer 
{
    public static void PlayAudio(AudioClip sound)
    {
        PlayAudio(sound,1,1);
    }
    public static void PlayAudio(AudioClip sound, float Volume)
    {
        PlayAudio(sound,Volume,1);
    }
    public static void PlayAudio(AudioClip sound,float Volume , float pitch)
    {
        GameObject Audioobject = new GameObject(sound.name+"-AudioPlayer");

        AudioSource palyer = Audioobject.AddComponent<AudioSource>();
        palyer.clip = sound;
        palyer.volume = Volume;
        palyer.pitch = pitch;
        palyer.spatialBlend = 0;
        palyer.Play();

        Object.Destroy(Audioobject, sound.length);
    }
}
