using UnityEngine;

public class Hoop : MonoBehaviour {
    
    public float colliderOffset;

    public CircleCollider2D leftCollider;
    public CircleCollider2D rightCollider;

    public float ballCheckOffset = 0.2f;
    public float ballCheckRadius = 0.4f;
    public LayerMask ballLayer;
    public float checkInterval = 1;

    private float checkCounter;

    public Vector3 BallCheckPos => transform.position + Vector3.down * ballCheckOffset;

    private void Start() {
        checkCounter = checkInterval;
    }

    private void Update() {
        if (checkCounter < 0) {
            CheckForBall();
            checkCounter = checkInterval;
        } else {
            checkCounter -= Time.deltaTime;
        }
    }

    private void CheckForBall() {

        Collider2D ballCol = Physics2D.OverlapCircle(BallCheckPos, ballCheckRadius, ballLayer);

        if (ballCol != null) {
            Ball ball = ballCol.GetComponent<Ball>();
            if (ball != null) {
                ball.Hooped();
                BallHooped();
            }
        }

    }

    private void BallHooped() {
        ScoreManager.Instance.AddScore(1);
    }

    [ContextMenu("Update Collider Sizes")]
    public void UpdateColliderSizes() {

        leftCollider.transform.position = transform.position - transform.right * colliderOffset;
        rightCollider.transform.position = transform.position + transform.right * colliderOffset;

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(BallCheckPos, ballCheckRadius);
    }

}