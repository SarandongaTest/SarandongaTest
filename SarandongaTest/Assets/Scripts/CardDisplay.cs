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

    void Awake() {
        if (card != null) {
            SetDisplays();
        }
    }

    /// <summary>
    /// Set the card information and update the display
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public GameObject SetCard(Card card) {
        this.card = card;
        SetDisplays();
        return this.gameObject;
    }

    /// <summary>
    /// Update the display
    /// </summary>
    public void SetDisplays() {
        name = card.name + GetInstanceID() ;
        cardDescription.text = card.description;
    }

    /// <summary>
    /// Instanciate a CardDisplay GameObject
    /// </summary>
    /// <param name="card"></param>
    /// <param name="cardPrefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject InstanciateCardDisplay(Card card, GameObject cardPrefab, GameObject parent) {
        return Instantiate(
            cardPrefab.GetComponent<CardDisplay>().SetCard(card),
            parent.transform);
    }
}
