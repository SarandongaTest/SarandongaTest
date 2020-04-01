using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Templates : MonoBehaviour {

    public static Templates instance;
    public GameObject whiteCardPrefab;
    public GameObject blackCardPrefab;
    public GameObject DeckSelectorPrefab;
    public static Deck deck;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
