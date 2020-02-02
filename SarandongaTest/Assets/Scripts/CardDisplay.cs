using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    public Card card;
    public Text cardDescription;

    public CardDisplay(Card card) {
        this.card = card;
    }

    void Start() {
        if (card != null) {
            SetDisplays();
        }
    }

    public GameObject SetCard(Card card) {
        this.card = card;
        SetDisplays();
        return this.gameObject;
    }

    public void SetDisplays() {
        cardDescription.text = card.description;
    }

    public static GameObject InstanciateCard(Card card, GameObject cardPrefab, GameObject parent) {
        return Instantiate(cardPrefab.GetComponent<CardDisplay>().SetCard(card), parent.transform);
    }
}
