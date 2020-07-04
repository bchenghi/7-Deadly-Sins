using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup audioMixer;
    public static AudioManager instance;

    void Awake() {

        if (instance == null){
            instance = this;
        }
        else {
            Destroy(this);
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.outputAudioMixerGroup = audioMixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    
    void Start() {
        // play theme?
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    // Can play sound from the gameobject of the transform.
    // Same sound can be played from multiple game objects. E.g. same attack sounds by enemies and player
    public void Play(Transform transformOfObject, string name) {
        StartCoroutine(PlayOnOtherGameObject(transformOfObject, name));
    }

    IEnumerator PlayOnOtherGameObject(Transform transformOfObject, string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound " + name + " not found!");
            yield return null;
        }
        AudioSource audioSource = transformOfObject.gameObject.AddComponent<AudioSource>();

        audioSource.clip = s.clip;
        audioSource.outputAudioMixerGroup = audioMixer;
        audioSource.volume = s.volume;
        audioSource.pitch = s.pitch;
        audioSource.loop = s.loop;
        
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(audioSource);
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 2f, s.volume / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitch/ 2f, s.pitch / 2f));

        s.source.Stop();
    }

    public void StopPlayingAll()
    {
        foreach(Sound s in sounds)
        {
            s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 2f, s.volume / 2f));
            s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitch / 2f, s.pitch / 2f));

            s.source.Stop();
        }
    }
}
