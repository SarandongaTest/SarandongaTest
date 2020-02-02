using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

    public static PlayerHand instance;
    public Dictionary<string, GameObject> hand = new Dictionary<string, GameObject>();

    private void Start() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
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
        float spacing = 2700 / Screen.width;
        int i = 0;
        foreach (string id in hand.Keys) {
            Vector3 position = new Vector3(spacing * i - spacing * (hand.Count - 1) / 2, 0, 0);
            hand[id].GetComponent<CardMovement>().SetPosition(transform.parent.transform.position + position);
            i++;
        }
    }
}
