using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Mixers")]
    public AudioMixerGroup soundMixer;
    public AudioMixerGroup trackMixer;

    [Header("Sound Options")]
    public float randomizePitchWindow = 0.1f;

    [Header("Tracks Options")]
    public float fadeTime = 3;

    public Sound[] sounds;
    public Sound[] tracks;

    public static AudioManager Instance;

    private List<Sound> playingTracks;
    private Dictionary<Sound, Coroutine> trackCoroutines;

    void Awake() {

        if (Instance == null)
            Instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = soundMixer;
        }

        trackCoroutines = new Dictionary<Sound, Coroutine>();
        foreach(Sound s in tracks) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = trackMixer;
            s.source.loop = true;
            
            if (s.introClip != null) {
                s.introSource = gameObject.AddComponent<AudioSource>();
                s.introSource.clip = s.introClip;
                s.introSource.volume = s.volume;
                s.introSource.pitch = s.pitch;
                s.introSource.outputAudioMixerGroup = trackMixer;

                s.introDuration = (double)s.introSource.clip.samples / s.introSource.clip.frequency;

            }

            trackCoroutines.Add(s, null);
        }

        playingTracks = new List<Sound>();
    }

    protected Sound FindSoundWithName(string name, Sound[] arr) {
        Sound s = Array.Find(arr, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found.");
            return null;
        }

        return s;
    }

    #region Sounds
    public void Play(string name) {
        Sound s = FindSoundWithName(name, sounds);

        if (s == null)
            return;

        if (s.randomizePitch) {
            s.source.pitch = s.pitch + UnityEngine.Random.Range(-randomizePitchWindow, randomizePitchWindow);
        }

        s.source.Play();
    }

    public void Pause(string name) {
        Sound s = FindSoundWithName(name, sounds);

        if (s == null)
            return;

        if (s.source.isPlaying) {
            s.source.Pause();
        }
    }
    #endregion

    #region Tracks
    public void PlayTrack(string name, bool fromStart = false, bool fadeIn = false, Sound s = null) {
        if (s == null) {
            s = FindSoundWithName(name, tracks);
            if (s == null)
                return;
        }

        if (playingTracks.Contains(s))
            return;

        if (trackCoroutines[s] != null) {
            StopCoroutine(trackCoroutines[s]);
        }

        ResetTrackVolumes(s);

        if (fromStart) {
            PlayLoopCheckingIntroFromStart(s);
        } else {
            if(s.introSource != null) {
                if (s.source.time == 0) {
                    PlayLoopCheckingIntroFromStart(s);
                } else {
                    s.source.UnPause();
                }
            } else {
                if (s.source.time == 0) {
                    s.source.Play();
                } else {
                    s.source.UnPause();
                }
            }
        }

        playingTracks.Add(s);

        if (fadeIn) {
            s.source.volume = 0;
            trackCoroutines[s] = StartCoroutine(VolumeFade(s.source, s.volume, s));
        }

    }

    private void ResetTrackVolumes(Sound s) {
        s.source.volume = s.volume;
        if (s.introSource != null) {
            s.introSource.volume = s.volume;
        }
    }

    private void PlayLoopCheckingIntroFromStart(Sound s) {
        s.source.Stop();
        if (s.introSource != null) {
            s.introSource.Stop();
        }

        PlayLoopCheckingIntro(s);
    }

    private void PlayLoopCheckingIntro(Sound s) {
        if (s.introClip != null) {
            double startTime = AudioSettings.dspTime + 0.5f;
            s.introSource.PlayScheduled(startTime);
            s.source.PlayScheduled(startTime + s.introDuration);
        } else {
            s.source.Play();
        }
    }

    public void PauseTrack(string name, bool fadeOut = false, Sound s = null) {

        if (s == null) {
            s = FindSoundWithName(name, tracks);
            if (s == null)
                return;
        }

        if(!playingTracks.Contains(s))
            return;
        
        if (trackCoroutines[s] != null) {
            StopCoroutine(trackCoroutines[s]);
        }

        playingTracks.Remove(s);

        AudioSource sourceToPause;
        
        if (s.introSource != null) {
            if (s.introSource.time != 0) {
                sourceToPause = s.introSource;
                s.source.Stop();
            } else {
                sourceToPause = s.source;
            }
        } else {
            sourceToPause = s.source;
        }

        if (fadeOut) {
            trackCoroutines[s] = StartCoroutine(VolumeFade(sourceToPause, 0, s));
        } else {
            sourceToPause.Pause();
        }

    }

    IEnumerator VolumeFade(AudioSource source, float finalVolume, Sound sound) {
        float time = 0;
        float initialVolume = source.volume;
        float step = (finalVolume - source.volume) / fadeTime;

        while(time < fadeTime) {
            source.volume = source.volume + step;
            time += Time.deltaTime;
            yield return null;
        }

        if (initialVolume > finalVolume) {
            source.Pause();
        }

        source.volume = finalVolume;

        trackCoroutines[sound] = null;

    }

    public void PauseCurrentTracks(bool fadeOut = false) {
        List<Sound> tracksToPause = new List<Sound>(playingTracks);
        foreach(Sound playingTrack in tracksToPause) {
            PauseTrack("", fadeOut, playingTrack);
        }
    }

    public bool IsTrackPlaying(string name) {
        Sound s = FindSoundWithName(name, tracks);
        if (s == null)
            return false;

        if (playingTracks.Contains(s)) {
            return true;
        } else {
            return false;
        }

    }
    #endregion
}
