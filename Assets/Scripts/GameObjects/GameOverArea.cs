using UnityEngine;
using UnityEngine.Events;

public class GameOverArea : MonoBehaviour {
    
    public UnityEvent onBallTrigger;

    private void OnTriggerEnter2D(Collider2D other) {
        Ball ball = other.GetComponent<Ball>();
        if (ball!= null) {
            if (ball.GetComponent<Rigidbody2D>().velocity.y < 0) {
                onBallTrigger?.Invoke();
            }
        }
    }

}