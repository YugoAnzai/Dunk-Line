using UnityEngine;

public class HoopsHolder : MonoBehaviour {
    
    public static HoopsHolder Instance;

    public static Transform Tr { get => GetTransform();}

    private void Awake() {
        Instance = this;
    }

    private static Transform GetTransform() {
        return Instance.transform;
    }

}