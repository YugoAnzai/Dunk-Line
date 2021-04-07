using UnityEngine;

public class PencilLinesHolder : MonoBehaviour {

    public static PencilLinesHolder Instance;

    public static Transform Tr { get => GetTransform();}

    private void Awake() {
        Instance = this;
    }

    private static Transform GetTransform() {
        return Instance.transform;
    }

}