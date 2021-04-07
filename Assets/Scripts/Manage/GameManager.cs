using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
    
    public static GameManager Instance;

    public List<SceneLoader.Scene> additiveScenes;

    public UnityEvent onGameOver;

    private void Awake() {
        Instance = this;
    }

    private void Start() {

        foreach(SceneLoader.Scene additiveScene in additiveScenes) {
            SceneLoader.LoadAdditive(additiveScene);
        }
        
    }

    public void GameOver() {
        onGameOver?.Invoke();
    }

}