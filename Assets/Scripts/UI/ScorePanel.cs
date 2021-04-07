using UnityEngine;

public class ScorePanel : MonoBehaviour {
    
    public TMPro.TextMeshProUGUI scoreCounter;

    public void UpdateScore(int newScore) {

        scoreCounter.text = newScore.ToString();
        // Feedback;

    }

}