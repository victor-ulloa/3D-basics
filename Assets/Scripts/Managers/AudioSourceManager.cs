using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

public class AudioSourceManager : MonoBehaviour {

    List<AudioSource> currentAudioSources = new List<AudioSource>();

    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup musicGroup;

    void Start() {
        currentAudioSources.Add(gameObject.GetComponent<AudioSource>());
    }

    public void Play(AudioClip clip, bool isMusic = false) {
        foreach (AudioSource source in currentAudioSources) {
            if (source.isPlaying) {
                continue;
            }

            source.PlayOneShot(clip);
            source.outputAudioMixerGroup = isMusic ? musicGroup : sfxGroup;
            return;
        }

        AudioSource temp = gameObject.AddComponent<AudioSource>();
        currentAudioSources.Add(temp);
        temp.PlayOneShot(clip);
        temp.outputAudioMixerGroup = isMusic ? musicGroup : sfxGroup;

    }
}
