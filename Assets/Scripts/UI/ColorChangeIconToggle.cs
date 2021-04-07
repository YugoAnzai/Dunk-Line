using UnityEngine;
using UnityEngine.UI;

public class ColorChangeIconToggle : ToggleIcon {

    public Color toggleOnColor;
    public Color toggleOffColor;

    private Image image;

    private void Start() {
        image = GetComponent<Image>();
    }

    public override void Toggle(bool toggleOn) {
        if (toggleOn) {
            image.color = toggleOnColor;
        } else {
            image.color = toggleOffColor;
        }
    }

}