using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool soundPlayer;

    // Start is called before the first frame update
    void Awake()
    {
     
        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            
        }

    }

    public void Play(string name, bool loop, float volume, float pitch) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.volume = volume;
        s.source.pitch = pitch;
        s.source.loop = loop;
        s.source.Play();
    }

    public void PlayOneShot(string name, bool loop, float volume, float pitch, AudioClip clip) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.volume = volume;
        s.source.pitch = pitch;
        s.source.loop = loop;
        s.source.PlayOneShot(clip);
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Stop();
    }

    public Sound GetSound(string name) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        return s;
    }

}
