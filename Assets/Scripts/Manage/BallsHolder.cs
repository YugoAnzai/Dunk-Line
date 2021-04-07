using UnityEngine;

public class BallsHolder : MonoBehaviour {

    public static BallsHolder Instance;

    public static Transform Tr { get => GetTransform();}

    private void Awake() {
        Instance = this;
    }

    private static Transform GetTransform() {
        return Instance.transform;
    }

}