using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

    public static PlayerHand instance;
    public Dictionary<string, GameObject> hand = new Dictionary<string, GameObject>();
    private GameObject selected = null;
    public float spacing = 2.5f;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Add a CardDisplay to the hand and reajust the positions
    /// </summary>
    /// <param name="card"></param>
    public void AddCard(GameObject card) {
        //If there is no card selected, the first one is

        hand.Add(card.name, card);

        if (selected == null) {
            SelectCard(card);
        }
        //SetCardsPositions();
    }


    public void SelectCard(GameObject card) {
        if (selected != null) {
            selected.GetComponent<WhiteCardDisplay>().SetSelected(false);
        }

        selected = card;
        card.GetComponent<WhiteCardDisplay>().SetSelected(true);
    }


    public void PlayCard() {
        RemoveCard(selected.name);
        Destroy(selected);
        GameController.Deal();
        SelectCard(transform.GetChild(0).gameObject);
    }

    /// <summary>
    /// Remove a CardDisplay of the hand and reajust the positions
    /// </summary>
    /// <param name="id"></param>
    public void RemoveCard(string id) {
        hand.Remove(id);
        //SetCardsPositions();
    }

    /// <summary>
    /// Set the position of all the cards
    /// </summary>
    private void SetCardsPositions() {
        int i = 0;
        foreach (string id in hand.Keys) {
            Vector3 position = new Vector3(spacing * i - spacing * (hand.Count - 1) / 2, 0, transform.position.z);
            hand[id].GetComponent<CardMovement>().SetPosition(transform.position + position);
            i++;
        }
    }
}
