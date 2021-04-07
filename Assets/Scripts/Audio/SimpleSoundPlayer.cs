using UnityEngine;

public class SimpleSoundPlayer : MonoBehaviour {
    
    private ObjectSoundManager soundManager;

    private void Start() {
        soundManager = GetComponentInChildren<ObjectSoundManager>();
    }

    public void PlaySound(string name) {
        soundManager.Play(name);
    }

}