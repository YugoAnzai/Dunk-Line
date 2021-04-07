using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour {
    
    public UnityEvent onHooped;

    public void Hooped() {
        // Feedbacks
        gameObject.SetActive(false);

        onHooped?.Invoke();

        onHooped.RemoveAllListeners();

    }

}