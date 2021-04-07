using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerPrefsIntDisplayer : MonoBehaviour {
    
    public SaveKey key;

    private void Start() {
        TextMeshProUGUI displayText = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefsManager.HasKey(key)) {
            displayText.text = PlayerPrefsManager.GetInt(key).ToString();
        } else {
            displayText.text = 0.ToString();
        }
    }

}