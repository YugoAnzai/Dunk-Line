using System;
using UnityEngine;

public class EndGameManager : MonoBehaviour {
    
    public GameObject endGamePanel;

    private void Start() {
        GameManager.Instance.onGameOver.AddListener(OnGameOver);
    }

    private void OnGameOver()
    {
        endGamePanel.SetActive(true);
    }
}