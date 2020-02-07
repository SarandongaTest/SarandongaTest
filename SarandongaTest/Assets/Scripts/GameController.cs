using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private void Start() {
        UIController.instance.SetBackgroundSize();
        UIController.instance.SetPlayerHandPosition();

        while (PlayerHand.instance.hand.Count < 10) {
            GameController.Deal();
        }
    }
    
    /// <summary>
    /// Instanciate a CardDisplay from a random JSON representation
    /// </summary>
    public static void Deal() {
        PlayerHand.instance.AddCard(
                        CardDisplay.InstanciateCardDisplay(Card.CardFromJSON(CardFileInterface.RandomLine()),
                        Templates.instance.cardPrefab,
                        PlayerHand.instance.gameObject));
    }
}
