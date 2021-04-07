using UnityEngine;

public class HoopPlacer : MonoBehaviour {
    
    public string hoopTag = "Hoop";

    private void Start() {
        Place();
    }

    public void Place() {
        GameObject hoop = ObjectPooler.Spawn(hoopTag, transform.position, transform.rotation, HoopsHolder.Tr);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 0.7f);
    }

}