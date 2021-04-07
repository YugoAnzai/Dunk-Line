using UnityEngine;

public class BallStopper : MonoBehaviour {
    
    private Ball stoppedBall;

    public void StopBall(Ball ball) {
        
        stoppedBall = ball;
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        Pencil pencil = FindObjectOfType<Pencil>();
        pencil.onEndLine.AddListener(ResumeStoppedBall);

    }

    public void ResumeStoppedBall() {
        Rigidbody2D rb = stoppedBall.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }

}