using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckItemController : MonoBehaviour {

    public Text deckNameTittle;
    public string deckPath;
    public Deck deck;
    public bool initialized = false;

    public void Setup(string path) {
        deckNameTittle.text = path.Substring(path.LastIndexOf("/") + 1); ;
        deckPath = path;
    }

    public void SetupFromWeb(string deckJSON) {
        initialized = true;
        JSONObjectInterface.ExtractFromJSON(deck, deckJSON);
        deckNameTittle.text = deck.deckName;
        GetComponent<Toggle>().isOn = true;
    }

    public void OnToogleChanged(bool toogled) {
        if (toogled && !initialized) {
            LoadDeck();
        }
    }

    private void LoadDeck() {
        initialized = true;
        JSONObjectInterface.ExtractFromJSON(
            deck, JSONFileInterface.ReadLine(deckPath + JSONPaths.fileName, 0));
    }
}
