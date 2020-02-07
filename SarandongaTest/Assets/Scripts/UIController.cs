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

    public void SetPlayerHandPosition() {
        playerHand.transform.position = GetWorldPositionOnPlane(new Vector3(Screen.width / 2, Screen.height / 5, 0), 0);
    }

    public void SetBackgroundSize() {
        background.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height * 2 / 5);
    }

    public void SetInfoCard(bool v, Card card = null) {
        image.SetActive(v);
        cardDisplay.SetActive(v);
        if(card != null)
        cardDisplay.GetComponent<CardDisplay>().SetCard(card);
    }

    public static Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        xy.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }
}
