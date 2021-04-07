using System.Collections.Generic;
using UnityEngine;

public class PencilLine : MonoBehaviour, IPooledObject {
    
    public float minNewPointDistance = 0.2f;

    private LineRenderer lineRend;
    private EdgeCollider2D edgeCollider;

    private List<Vector2> linePoints;

    private bool isSetup = false;

    private void Setup() {
        if (isSetup)
            return;

        lineRend = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        isSetup = true;

    }

    public void OnObjectSpawn()
    {
        Setup();

        linePoints = new List<Vector2>();

        lineRend.positionCount = 0;

        edgeCollider.points = new Vector2[0];

    }

    public void UpdateLine(Vector2 pencilPos) {

        if (linePoints.Count == 0) {
            CreateNewPoint(pencilPos);
        } else if (Vector2.Distance(linePoints[linePoints.Count - 1], pencilPos) > minNewPointDistance) {
            CreateNewPoint(pencilPos);
        }

    }

    private void CreateNewPoint(Vector2 newPointPos) {

        linePoints.Add(newPointPos);

        lineRend.positionCount = linePoints.Count;
        lineRend.SetPosition(linePoints.Count - 1, newPointPos);

        UpdateCollider();

    }

    private void UpdateCollider() {
        
        if (linePoints.Count != 1) {
            edgeCollider.points = linePoints.ToArray();
        }
        
    }

}