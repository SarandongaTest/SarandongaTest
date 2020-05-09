using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

    //public Rect radarScreen = new Rect(Screen.width * 0.86f, Screen.height * 0.01f, 180, 385);
    public Vector2 scrollPosition = Vector2.zero;

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

    public void PlayCardTurn(bool playCardTurn) {
        PhoneButtonsController.instance.SetPlayButtons(playCardTurn);
        foreach (GameObject card in hand) {
            card.SetActive(playCardTurn);
        }
        if (playCardTurn) {
            //SelectCard(cardsToDecide[0]);
            foreach (GameObject cardGameObject in cardsToDecide) {
                Destroy(cardGameObject);
            }
            cardsToDecide.Clear();
            SelectCard(hand[0]);
        }
    }

    public void AddSelectionCard(string card) {
        cardsToDecide.Add(CardDisplayWhite.InstanciateCardDisplay(
            JSONObjectInterface.BuildFromJSON<CardWhite>(card),
            gameObject));
    }

    /*public void PlayCardTurn(bool playCardTurn, string[] cards) {
        //PhoneButtonsController.instance.SetPlayButtons(playCardTurn);
        if (!playCardTurn) {
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

    }*/

    public void PlayCard() {
        PhoneButtonsController.instance.SetPlayInactive();
        RemoveCard(selected);
        GameController.instance.SendCard(selected);
        Destroy(selected);
    }

    public void DecideCard() {
        GameController.instance.SendCard(selected);

    }
}
