using UnityEngine;
using UnityEngine.Audio;
using System;
using Unity.VisualScripting;

public class SAudioManager : MonoBehaviour
{

    public SSound[] sounds;
    void Awake()
    {
        foreach (SSound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
        }

    }
    void Start()
    {
        Play("Testing");
    }

    public void Play(string name)
    {
        SSound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound Ain't Working");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        SSound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Stopping");
            return;
        }

        s.source.Stop();
    }
    public bool IsPlaying(string name)
    {
        SSound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
            return false;
        }

        return s.source.isPlaying;
    }


    public void SetVolume(string name, float volume)
    {
        SSound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.volume = volume;
        }
    }

    public float GetVolume(string name)
    {
        SSound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            return s.source.volume;
        }
        return 0f;
    }
}

