using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour {
    
    public static GameLevelManager Instance;

    public bool playFirstLevelAtStart;
    public SceneLoader.Scene firstLevel;
    public float setupLevelDelay = 0.2f;
    public float delayBetweenLevels = 1;

    public List<SceneLoader.Scene> levels;

    private SceneLoader.Scene currentLevel;

    private int ballThrowersQuant;
    private int hoopedBallsQuant;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        if (playFirstLevelAtStart)
            LoadLevel(firstLevel);
    }

    private SceneLoader.Scene GetRandomLevel() {
        return Rng.ChooseInList(levels);
    }

    public void LoadLevel(SceneLoader.Scene level) {

        SceneLoader.LoadAdditive(level);

        currentLevel = level;

        Invoke(nameof(SetupLevel), setupLevelDelay);

    }

    public void LoadRandomLevel() {
        LoadLevel(GetRandomLevel());
    }

    public void SetupLevel() {

        BallThrower[] ballThrowers = FindObjectsOfType<BallThrower>();
        ballThrowersQuant = ballThrowers.Length;
        foreach(BallThrower ballThrower in ballThrowers) {
            ballThrower.onThrow.AddListener(OnThrowBall);
        }

    }

    public void OnThrowBall(Ball ball) {
        ball.onHooped.AddListener(OnBallHooped);
    }

    public void OnBallHooped() {
        hoopedBallsQuant++;
        
        if (hoopedBallsQuant == ballThrowersQuant) {
            CompleteLevel();
        }

    }

    private void CompleteLevel() {

        ClearLevel();

        SceneLoader.UnloadAdditive(currentLevel);

        Invoke(nameof(LoadRandomLevel), delayBetweenLevels);

    }

    public void ClearLevel() {

        foreach(Transform child in PencilLinesHolder.Tr) {
            child.gameObject.SetActive(false);
        }
        foreach(Transform child in HoopsHolder.Tr) {
            child.gameObject.SetActive(false);
        }
        foreach(Transform child in BallsHolder.Tr) {
            child.gameObject.SetActive(false);
        }

        ballThrowersQuant = 0;
        hoopedBallsQuant = 0;

    }

}