using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

    public static PlayerHand instance;
    public GameObject background;
    public Dictionary<string, GameObject> hand = new Dictionary<string, GameObject>();

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        //todo pasar a un controller
        /******************************************************/
        RectTransform tf = background.GetComponent<RectTransform>();
        tf.sizeDelta = new Vector2(0, Screen.height*2/5);

        //transform.position = background.transform.position;
        Vector3 ran = new Vector3(Screen.width / 2, Screen.height/5, 0);
        Debug.Log(ran);
        Vector3 pos = Camera.main.ScreenToWorldPoint(ran);
        pos = CardMovement.GetWorldPositionOnPlane(ran, 0);
        Debug.Log(pos);
        pos.z = 0;
        transform.position = pos;
        /******************************************************/

    }

    public void AddCard(GameObject card) {
        hand.Add(card.name, card);
        setCardsPositions();
    }

    public void RemoveCard(string id) {
        hand.Remove(id);
        setCardsPositions();
    }

    private void setCardsPositions() {
        float spacing = 2.5f;
        int i = 0;
        foreach (string id in hand.Keys) {
            Vector3 position = new Vector3(spacing * i - spacing * (hand.Count - 1) / 2, 0, 0);
            hand[id].GetComponent<CardMovement>().SetPosition(transform.position + position);
            i++;
        }
    }
}
