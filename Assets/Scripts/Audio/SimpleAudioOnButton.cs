using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SimpleAudioOnButton : SimpleAudioPlayerComponent {
    
    [SerializeField]
    private bool playAudio;
    public string audioName;

    private void Start() {
        if (playAudio) {
            GetComponent<Button>().onClick.AddListener(delegate{PlayAudio(audioName);});
        }
    }

}