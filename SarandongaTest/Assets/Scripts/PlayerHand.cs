using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

    public static PlayerHand instance;

    public List<GameObject> hand = new List<GameObject>();
    private GameObject selected = null;
    internal static int maxCards = 10;

    private List<GameObject> cardsToDecide = new List<GameObject>();

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
        hand.Add(card);

        if (selected == null) {
            SelectCard(card);
        }
    }

    public void AddCard(CardWhite card) {
        GameObject cardInstance = CardDisplayWhite.InstanciateCardDisplay(card,
                gameObject);
        AddCard(cardInstance);
    }

    /// <summary>
    /// Remove a CardDisplay of the hand and reajust the positions
    /// </summary>
    /// <param name="id"></param>
    public void RemoveCard(GameObject card) {
        hand.Remove(card);
    }

    public void SelectCard(GameObject card) {
        if (selected != null)
            selected.GetComponent<CardDisplayWhite>().SetSelected(false);

        selected = card;
        card.GetComponent<CardDisplayWhite>().SetSelected(true);
    }

    public void SelectCardTurn(bool selectCardTurn, string[] cards) {
        foreach (GameObject card in hand) {
            card.SetActive(!selectCardTurn);
        }
        if (selectCardTurn) {
            foreach (string cardToDecide in cards) {
                cardsToDecide.Add(CardDisplayWhite.InstanciateCardDisplay(
                    JSONObjectInterface.BuildFromJSON<CardWhite>(cardToDecide),
                    gameObject));
            }
            SelectCard(cardsToDecide[0]);
        } else {
            foreach (GameObject cardGameObject in cardsToDecide) {
                Destroy(cardGameObject);
            }
            cardsToDecide.Clear();
            SelectCard(hand[0]);
        }

    }

    public void PlayCard() {
        RemoveCard(selected);
        GameController.instance.SendCard(selected);
        Destroy(selected);
        SelectCard(transform.GetChild(0).gameObject);
    }
}
