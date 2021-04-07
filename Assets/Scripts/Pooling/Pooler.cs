using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
        public bool expand = false;
    }

    public static Pooler Instance;

    public bool expandable = true;

    public List<Pool> general;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    protected List<List<Pool>> allPoolLists;
    protected Dictionary<string, GameObject> tagPrefabExpansionDictionary;

    protected virtual void AddLists() {
        allPoolLists.Add(general);
    }

    protected virtual void SingletonAttribution() {
        Instance = this;
    }

    protected virtual void Awake() {

        if (Instance == null) {
            SingletonAttribution();
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        tagPrefabExpansionDictionary = new Dictionary<string, GameObject>();

        // Add other pools List
        allPoolLists = new List<List<Pool>>();
        AddLists();

        // StartCoroutine(InstantiateObjectsOverTime());
        foreach(List<Pool> poolList in allPoolLists) {
            foreach(Pool pool in poolList) {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                EnqueuePrefab(objectPool, pool.prefab, pool.size);

                poolDictionary.Add(pool.tag, objectPool);
                if (pool.expand) {
                    tagPrefabExpansionDictionary.Add(pool.tag, pool.prefab);
                }

            }
        }

    }

    protected virtual void EnqueuePrefab(Queue<GameObject> queue, GameObject prefab, int quantity) {
        for (int i = 0; i < quantity; i++) {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }
    }

    protected virtual GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Transform newParentTransform) {

        if (!poolDictionary.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Peek();
        
        if (expandable && objectToSpawn.activeSelf && tagPrefabExpansionDictionary.ContainsKey(tag)) {

            Queue<GameObject> temp = new Queue<GameObject>();
            int size = poolDictionary[tag].Count;

            EnqueuePrefab(temp, tagPrefabExpansionDictionary[tag], size);

            for(int i=0; i < size; i++) {
                temp.Enqueue(poolDictionary[tag].Dequeue());
            }

            poolDictionary[tag] = temp;

            Debug.Log("Pooler expansion. tag :" + tag + " by " + size +". have " + temp.Count);

        }

        objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(false);
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.SetParent(newParentTransform);

        IPooledObject[] pooledObjs = objectToSpawn.GetComponents<IPooledObject>();
        foreach(IPooledObject pooledObj in pooledObjs) {
            pooledObj.OnObjectSpawn();
        }
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;

    }

}
