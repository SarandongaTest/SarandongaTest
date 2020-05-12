using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayBlack : CardDisplay {

    public CardBlack  card;
    public Text pickText;

    /// <summary>
    /// Set the card information and update the display
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public GameObject SetCard(CardBlack card) {
        this.card = card;
        SetDisplays();
        return this.gameObject;
    }

    /// <summary>
    /// Update the display
    /// </summary>
    public override void SetDisplays() {
        name = GetInstanceID().ToString();
        cardDescription.text = card.text;
        pickText.text = "Pick " + card.pick;
    }

    /// <summary>
    /// Instanciate a CardDisplay GameObject
    /// </summary>
    /// <param name="card"></param>
    /// <param name="cardPrefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject InstanciateCardDisplay(CardBlack  card, GameObject cardPrefab, GameObject parent) {
        return Instantiate(
            cardPrefab.GetComponent<CardDisplayBlack>().SetCard(card),
            parent.transform);
    }
}
