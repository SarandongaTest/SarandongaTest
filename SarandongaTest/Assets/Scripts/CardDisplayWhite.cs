using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayWhite : CardDisplay {

    public CardWhite card;
    internal bool selected = false;

    /// <summary>
    /// Set the card information and update the display
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public GameObject SetCard(CardWhite card) {
        this.card = card;
        SetDisplays();
        return this.gameObject;
    }

    /// <summary>
    /// Update the display
    /// </summary>
    public override void SetDisplays() {
        name = /*card.name +*/ GetInstanceID() + "";
        cardDescription.text = card.text;
    }

    /// <summary>
    /// Instanciate a CardDisplay GameObject
    /// </summary>
    /// <param name="card"></param>
    /// <param name="cardPrefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject InstanciateCardDisplay(CardWhite card, GameObject parent) {
        return Instantiate(
            Templates.instance.whiteCardPrefab.GetComponent<CardDisplayWhite>().SetCard(card),
            parent.transform);
    }

    /// <summary>
    /// Set if the card is the selected one
    /// </summary>
    /// <param name="selected"></param>
    public void SetSelected(bool selected) {
        if (this.selected == selected)
            return;

        this.transform.localScale = selected ? new Vector2(1.2f, 1.2f) : Vector2.one;
        this.gameObject.GetComponent<Image>().color = selected ? new Color(1, 1, 1) : new Color(0.85f, 0.85f, 0.85f);
        this.selected = selected;
    }

    public void CardClicked() {
        PlayerHand.instance.SelectCard(this.gameObject);
    }
}
