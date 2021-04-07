using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntUnityEvent : UnityEvent<int> {
}

public class Pencil : MonoBehaviour {
    
    public string lineTag;

    public UnityEvent onStartLine;
    public UnityEvent onEndLine;

    public IntUnityEvent onTotalChargesChangePassTotalCharges;
    public IntUnityEvent onChargesChangePassCharges;

    private int totalCharges;
    private int charges;
    private Camera cam;
    private PencilLine currentLine;

    public int TotalCharges => totalCharges;
    public int Charges => charges;

    private void Start() {
        cam = Camera.main;
    }

    private void Update() {

        DrawLineUpdate();

    }

    #region Line Charges
    public void SetTotalCharges(int _totalCharges) {
        totalCharges = _totalCharges;

        charges = totalCharges;

        onTotalChargesChangePassTotalCharges?.Invoke(totalCharges);
        onChargesChangePassCharges?.Invoke(charges);
    }
    #endregion

    #region Draw Line

    private void DrawLineUpdate() {

        List<Touch> touches = InputHelper.GetTouches();

        if (touches.Count == 0)
            return;

        Touch touch = touches[0];

        if (touch.phase == TouchPhase.Began) {
            if (charges > 0) {
                StartLine(touch.position);
            }
        } else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended) {
            if (currentLine != null) {
                EndLine();
            }
        } else {
            if (currentLine != null) {
                UpdateLine(touch.position);
            }
        }

    }

    private void StartLine(Vector2 touchPosition) {

        charges--;
        onChargesChangePassCharges?.Invoke(charges);

        onStartLine?.Invoke();

        GameObject pencilLineObj = ObjectPooler.Spawn(lineTag, PencilLinesHolder.Tr);
        currentLine = pencilLineObj.GetComponent<PencilLine>();

        currentLine.UpdateLine(cam.ScreenToWorldPoint(touchPosition));

    }

    private void UpdateLine(Vector2 touchPosition) {
        currentLine.UpdateLine(cam.ScreenToWorldPoint(touchPosition));
    }

    private void EndLine() {
        currentLine = null;
        onEndLine?.Invoke();
    }

    #endregion

}