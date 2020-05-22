using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour {

    public static PopupController instance;
    public Text popupTittle;
    public Text popupMessage;

    private void Awake() {
        instance = this;
        gameObject.SetActive(false);
    }

    void Update() {
        if (Application.platform == RuntimePlatform.Android) {
            if (Input.touchCount > 0) {
                gameObject.SetActive(false);
            }
        } else {
            if (Input.GetMouseButtonDown(0)) {
                gameObject.SetActive(false);
            }
        }
    }

    public void Setup(string message, string tittle = "New turn") {
        popupTittle.text = tittle;
        popupMessage.text = message;
        gameObject.SetActive(true);
    }
}
