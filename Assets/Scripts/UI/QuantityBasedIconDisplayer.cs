using System;
using System.Collections.Generic;
using UnityEngine;

public class QuantityBasedIconDisplayer : MonoBehaviour {
    
    public Transform togglesHolder;
    public float checkQuantityOnStartDelay = 0.3f;

    private List<ToggleIcon> toggles;

    protected virtual void Start() {

        toggles = new List<ToggleIcon>();

        foreach(Transform toggleTr in togglesHolder) {
            toggles.Add(toggleTr.GetComponent<ToggleIcon>());
        }

        Invoke(nameof(CheckQuantityAndUpdate), checkQuantityOnStartDelay);

    }

    protected virtual void CheckQuantityAndUpdate() {

    }

    public void UpdateTotalToggles(int totalQuantity) {
        int count = 0;
        foreach(ToggleIcon toggle in toggles) {
            if (count < totalQuantity) {
                toggle.gameObject.SetActive(true);
            } else {
                toggle.gameObject.SetActive(false);
            }
            count++;
        }
    }

    public void UpdateToggles(int quantity) {
        
        for(int i = 0; i < toggles.Count; i++) {
            if(i < quantity) {
                toggles[i].Toggle(true);
            } else {
                toggles[i].Toggle(false);
            }
        }

    }

}