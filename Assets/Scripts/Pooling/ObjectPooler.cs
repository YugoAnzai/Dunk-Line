using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Pooler
{

    [HideInInspector] public new static ObjectPooler Instance;

    public List<Pool> main;
    public List<Pool> effects;

    protected override void SingletonAttribution() {
        Instance = this;
    }

    protected override void AddLists() {
        base.AddLists();
        allPoolLists.Add(main);
        allPoolLists.Add(effects);
    }

    public static GameObject Spawn(string tag, Vector3 position, Quaternion rotation, Transform newParentTransform) {
        return Instance.SpawnFromPool(tag, position, rotation, newParentTransform);
    }

    public static GameObject Spawn(string tag, Vector3 position, Transform newParentTransform) {
        return Instance.SpawnFromPool(tag, position, Quaternion.identity, newParentTransform);
    }

    public static GameObject Spawn(string tag, Transform newParentTransform) {
        return Instance.SpawnFromPool(tag, Vector3.zero, Quaternion.identity, newParentTransform);
    }

    public static GameObject Spawn(string tag, Vector3 position) {
        return Instance.SpawnFromPool(tag, position, Quaternion.identity, Instance.transform.parent);
    }

    public static GameObject Spawn(string tag) {
        return Instance.SpawnFromPool(tag, Vector3.zero, Quaternion.identity, Instance.transform.parent);
    }

}
