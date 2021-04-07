using System;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnPencilStart : MonoBehaviour {
    
    public List<GameObject> objectsToDeactivate;

    private void Start() {
        Pencil pencil = FindObjectOfType<Pencil>();

        pencil.onStartLine.AddListener(HideObjects);

    }

    private void HideObjects()
    {
        
        foreach(GameObject obj in objectsToDeactivate) {
            obj.SetActive(false);
        }

    }
}