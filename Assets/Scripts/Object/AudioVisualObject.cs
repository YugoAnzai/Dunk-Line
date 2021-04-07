using UnityEngine;

public class AudioVisualObject : MonoBehaviour {
    
    protected Animator animator;
    protected AnimatorHelper animatorHelper;
    protected ObjectSoundManager soundManager;

    protected virtual void Awake() {
        GetComponentsSetup();
    }

    protected virtual void GetComponentsSetup() {
        animator = GetComponentInChildren<Animator>();
        animatorHelper = GetComponentInChildren<AnimatorHelper>();

        soundManager = GetComponentInChildren<ObjectSoundManager>();
    }

}