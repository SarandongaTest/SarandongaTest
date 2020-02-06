using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject background;
    public GameObject playerHand;
    public GameObject image;
    public GameObject cardDisplay;


    public static UIController instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    void Start() {
        SetBackgroundSize();
        SetPlayerHandPosition();
    }

    private void SetPlayerHandPosition() {
        Vector3 pos = CardMovement.GetWorldPositionOnPlane(new Vector3(Screen.width / 2, Screen.height / 5, 0), 0);
        pos.z = 0;
        playerHand.transform.position = pos;
    }

    private void SetBackgroundSize() {
        background.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height * 2 / 5);
    }

    public void SetInfoCard(bool v, Card card = null) {
        image.SetActive(v);
        cardDisplay.SetActive(v);
        if(card != null)
        cardDisplay.GetComponent<CardDisplay>().SetCard(card);
    }
}
