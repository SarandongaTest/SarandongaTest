using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteCardDisplay : CardDisplay {

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
