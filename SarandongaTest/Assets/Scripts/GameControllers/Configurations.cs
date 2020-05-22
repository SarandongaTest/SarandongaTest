using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurations : MonoBehaviour {

    public static Configurations instance;
    public GameObject whiteCardPrefab;
    public GameObject blackCardPrefab;
    public GameObject DeckItemPrefab;
    public GameObject lobbyPlayerPrefab;
    public static Deck deck;
    public static int players;
    internal static string playerName = "default";

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
