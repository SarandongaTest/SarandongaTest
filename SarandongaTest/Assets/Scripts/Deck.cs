using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Deck {
    public string deckName;
    public List<CardWhite> whiteCards = new List<CardWhite>();
    public List<CardBlack> blackCards = new List<CardBlack>();

    private List<CardWhite> toDealWhite = null;
    private List<CardBlack> toDealBlack = null;

    public Deck(List<Deck> decks) {
        foreach (Deck deck in decks) {
            whiteCards.AddRange(deck.whiteCards);
            blackCards.AddRange(deck.blackCards);
        }
        InitializeDecks();
    }

    public Deck() {
        InitializeDecks();
    }

    #region Cards management
    public void AddWhiteCard(CardWhite card) {
        whiteCards.Add(card);
    }

    public void RemoveWhiteCard(CardWhite card) {
        whiteCards.Remove(card);
    }

    public void AddBlackCard(CardBlack card) {
        blackCards.Add(card);
    }

    public void RemoveBlackCard(CardBlack card) {
        blackCards.Remove(card);
    }
    #endregion

    #region Deal cards
    public CardWhite DealCard() {
        if (toDealWhite.Count <= 0) {
            InitializeWhiteDeck();
        }

        CardWhite card = toDealWhite[UnityEngine.Random.Range(0, toDealWhite.Count)];
        toDealWhite.Remove(card);
        return card;
    }

    public CardBlack DealBlackCard() {
        if (toDealBlack.Count <= 0) {
            InitializeBlackDeck();
        }


        CardBlack card = toDealBlack[UnityEngine.Random.Range(0, toDealBlack.Count)];
        toDealBlack.Remove(card);
        return card;
    }
    #endregion

    public void InitializeDecks() {
        InitializeWhiteDeck();
        InitializeBlackDeck();
    }

    private void InitializeBlackDeck() {
        toDealBlack = new List<CardBlack>(blackCards);
    }

    private void InitializeWhiteDeck() {
        toDealWhite = new List<CardWhite>(whiteCards);
    }
}
