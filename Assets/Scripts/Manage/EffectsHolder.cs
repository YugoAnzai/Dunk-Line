using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsHolder : MonoBehaviour
{
    public static EffectsHolder Instance;

    public static Transform Tr { get => GetTransform();}

    private void Awake() {
        Instance = this;
    }

    private static Transform GetTransform() {
        return Instance.transform;
    }

}
