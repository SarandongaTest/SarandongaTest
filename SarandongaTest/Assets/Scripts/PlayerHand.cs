using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

    public Vector2 scrollPosition = Vector2.zero;

    public static PlayerHand instance;

    public int score = 0;
    public List<GameObject> hand = new List<GameObject>();
    private GameObject selected = null;
    internal static int maxCards = 10;
    private bool playingCard = true;

    private List<GameObject> cardsToDecide = new List<GameObject>();

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void AddCard(CardWhite card) {
        GameObject cardInstance = CardDisplayWhite.InstanciateCardDisplay(card,
                gameObject);
        AddCard(cardInstance);
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
        card.SetActive(playingCard);
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

    /// <summary>
    /// Set the UI to a selecting or not
    /// </summary>
    /// <param name="playCardTurn"></param>
    public void BasicPlayCardTurn(bool playCardTurn = false) {
        playingCard = playCardTurn;
        UIController.instance.SetPlayButtons(playCardTurn);
        foreach (GameObject card in hand) {
            card.SetActive(playCardTurn);
        }
    }

    public void TriggerPlayCardTurn(bool playCardTurn) {
        BasicPlayCardTurn(playCardTurn);
        if (playCardTurn) {
            foreach (GameObject cardGameObject in cardsToDecide) {
                Destroy(cardGameObject);
            }
            cardsToDecide.Clear();
            SelectCard(hand[0]);
        } else {
            score++;
            UIController.instance.UpdateScoreText(score);
        }
    }

    public void ProceedToDecideCard() {
        UIController.instance.SetDecideInteractable();
    }

    public void AddSelectionCard(string card) {
        cardsToDecide.Add(CardDisplayWhite.InstanciateCardDisplay(
            JSONObjectInterface.BuildFromJSON<CardWhite>(card),
            gameObject));
        if (cardsToDecide.Count == 1) {
            SelectCard(cardsToDecide[0]);
        }
    }

    public void PlayCard() {
        UIController.instance.SetPlayButtonAndTittle(false);
        RemoveCard(selected);
        GameController.instance.SendCard(selected);
        Destroy(selected);
    }


    public void SelectWinnerCard() {
        GameController.instance.SendCard(selected);

    }

    public void PlayNewHand() {
        if (!playingCard) return;
        UIController.instance.SetPlayButtonAndTittle(true);
    }
}
