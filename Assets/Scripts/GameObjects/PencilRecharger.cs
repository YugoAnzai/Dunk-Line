using UnityEngine;

public class PencilRecharger : MonoBehaviour {
    
    public int totalCharges = 1;
    public bool rechargeOnStart = true;

    private void Start() {
        if (rechargeOnStart) {
            RechargeExistingPencil();
        }
    }

    public void RechargeExistingPencil() {
        Pencil pencil = FindObjectOfType<Pencil>();
        pencil.SetTotalCharges(totalCharges);
    }

}