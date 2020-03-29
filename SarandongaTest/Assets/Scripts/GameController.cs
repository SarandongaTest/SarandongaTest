using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public static Deck deck;

    public GameObject blackCard;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        /*deck = JSONObjectInterface.BuildFromJSON<Deck>(JSONFileInterface.RandomLine("Base/" + JSONPaths.fileName));
        */
        while (PlayerHand.instance.hand.Count < 10) {
            Deal();
        }

        blackCard.GetComponent<CardDisplayBlack>().SetCard(deck.DealBlackCard());
    }

    /// <summary>
    /// Instanciate a CardDisplay from a random JSON representation
    /// </summary>
    public static void Deal() {
        PlayerHand.instance.AddCard(
            CardDisplayWhite.InstanciateCardDisplay(deck.DealCard(),
            Templates.instance.whiteCardPrefab,
            PlayerHand.instance.gameObject));
    }
}
