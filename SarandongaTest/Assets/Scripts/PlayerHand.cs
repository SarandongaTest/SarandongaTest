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
        }
    }

    /// <summary>
    /// Add a CardDisplay to the hand and reajust the positions
    /// </summary>
    /// <param name="card"></param>
    public void AddCard(GameObject card) {
        hand.Add(card.name, card);

        if (selected == null) {
            SelectCard(card);
        }
    }

    /// <summary>
    /// Remove a CardDisplay of the hand and reajust the positions
    /// </summary>
    /// <param name="id"></param>
    public void RemoveCard(string id) {
        hand.Remove(id);
    }

    public void SelectCard(GameObject card) {
        if (selected != null)
            selected.GetComponent<CardDisplayWhite>().SetSelected(false);

        selected = card;
        card.GetComponent<CardDisplayWhite>().SetSelected(true);
    }

    public void PlayCard() {
        /*RemoveCard(selected.name);
        Destroy(selected);
        GameController.Deal();
        SelectCard(transform.GetChild(0).gameObject);*/
    }
}
