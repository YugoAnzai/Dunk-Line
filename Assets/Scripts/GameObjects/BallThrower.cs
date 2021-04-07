using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnThrowEvent : UnityEvent<Ball> {

}

public class BallThrower : MonoBehaviour {
    
    public string ballTag = "Ball";
    public float throwDelay;
    public float throwForce;

    private bool hasThrown;

    public OnThrowEvent onThrow;

    public bool HasThrown => hasThrown;

    public void Start() {
        hasThrown = false;
        Invoke(nameof(SpawnAndThrow), throwDelay);
    }

    public void SpawnAndThrow() {
        
        GameObject ballGO = ObjectPooler.Spawn(ballTag, transform.position, BallsHolder.Tr);
        Ball ball = ballGO.GetComponent<Ball>();
        Rigidbody2D ballRb = ballGO.GetComponent<Rigidbody2D>();

        ballRb.AddForce(transform.up * throwForce, ForceMode2D.Impulse);

        hasThrown = true;

        onThrow?.Invoke(ball);

        onThrow.RemoveAllListeners();

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * throwForce / 1.5f);
    }

}