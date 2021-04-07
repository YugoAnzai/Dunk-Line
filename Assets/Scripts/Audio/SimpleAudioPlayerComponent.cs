using UnityEngine;

public class SimpleAudioPlayerComponent : MonoBehaviour {
    
    public void PlayAudio(string name) {
        if(AudioManager.Instance) {
            AudioManager.Instance.Play(name);
        }
    }

}