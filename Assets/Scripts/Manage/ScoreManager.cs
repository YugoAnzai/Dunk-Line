using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ScoreChangeCallback : UnityEvent<int> {

}

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;

    public ScoreChangeCallback onScoreChangePassScore;

    private int score;

    public int Score => score;

    private void Awake()
    {

        Instance = this;

    }

    public void AddScore(int add)
    {

        score += add;

        onScoreChangePassScore?.Invoke(score);

    }

    public void SaveScores()
    {

        PlayerPrefsManager.SetInt(SaveKey.LastScore, score);

        if (!PlayerPrefsManager.HasKey(SaveKey.HighScore)
        || PlayerPrefsManager.GetInt(SaveKey.HighScore) < score)
        {
            PlayerPrefsManager.SetInt(SaveKey.HighScore, score);
        }

    }

}