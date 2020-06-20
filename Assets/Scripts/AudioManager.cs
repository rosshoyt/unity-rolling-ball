using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        FindSound(name).source.Play();
    }
    public void Stop(string name)
    {
        FindSound(name).source.Stop();
    }

    public void SetVolume(string name, float vol)
    {
        //Debug.Log("Setting volume to " + vol.ToString());
        Sound s = FindSound(name);
        s.volume = vol;
        s.source.volume = vol;
    }

    /**
     * Changes volume of a sound by specified amount
     * @param vol float from 1.0f to (-1.0f)
     */ 
    public void ChangeVolume(string name, float vol)
    {
        Sound s = FindSound(name);
        float newVol = s.volume + vol;
        Debug.Log("Changing volume to " + newVol.ToString());
        s.volume = newVol;
        s.source.volume = newVol;
    }

    public float GetVolume(string name)
    {
        //Sound s = FindSound(name);//.source.volume;
        return Array.Find(sounds, sound => sound.name == name).source.volume;
        //Array.Find(sounds, sound => sound.name == name);

    }

    private Sound FindSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }
}
