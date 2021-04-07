using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;

    public AudioClip clip;
    public AudioClip introClip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool randomizePitch;

    [HideInInspector]
    public AudioSource source;
    [HideInInspector]
    public AudioSource introSource;

    [HideInInspector]
    public double introDuration;
    
}
