using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ObjectSoundManager : MonoBehaviour {
    
    [System.Serializable]
    public class AudioSourceName {
        public string name;
        public AudioSource source;
        public bool randomizePitch;
        [HideInInspector]
        public float originalPitch;
    }

    [Header("Sound Options")]
    public float randomizePitchWindow = 0.1f;

    public List<AudioSourceName> audioSourceNames;

    private void Start() {
        foreach(AudioSourceName asn in audioSourceNames) {
            asn.originalPitch = asn.source.pitch;
        }
    }

    protected AudioSourceName FindSoundWithName(string name) {
        AudioSourceName asn = audioSourceNames.Find(item => item.name == name);
        if (asn == null) {
            Debug.LogWarning("Sound: " + name + " not found.");
            return null;
        }

        return asn;
    }

    public void Play(string name) {
        AudioSourceName asn = FindSoundWithName(name);

        if (asn == null)
            return;

        if (asn.randomizePitch) {
            asn.source.pitch = asn.originalPitch + UnityEngine.Random.Range(-randomizePitchWindow, randomizePitchWindow);
        }

        asn.source.Play();

    }

}