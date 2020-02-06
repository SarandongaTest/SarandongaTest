using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    
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
