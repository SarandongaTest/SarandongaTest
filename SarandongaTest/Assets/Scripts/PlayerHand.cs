using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

    public static PlayerHand instance;
    public Dictionary<string, GameObject> hand = new Dictionary<string, GameObject>();
    public float spacing = 2.5f;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        while (hand.Count < 10) {
            GameController.Deal();
        }
    }

    /// <summary>
    /// Add a CardDisplay to the hand and reajust the positions
    /// </summary>
    /// <param name="card"></param>
    public void AddCard(GameObject card) {
        hand.Add(card.name, card);
        setCardsPositions();
    }

    /// <summary>
    /// Remove a CardDisplay of the hand and reajust the positions
    /// </summary>
    /// <param name="id"></param>
    public void RemoveCard(string id) {
        hand.Remove(id);
        setCardsPositions();
    }

    /// <summary>
    /// Set the position of all the cards
    /// </summary>
    private void setCardsPositions() {
        int i = 0;
        foreach (string id in hand.Keys) {
            Vector3 position = new Vector3(spacing * i - spacing * (hand.Count - 1) / 2, 0, 0);
            hand[id].GetComponent<CardMovement>().SetPosition(transform.position + position);
            i++;
        }
    }
}
