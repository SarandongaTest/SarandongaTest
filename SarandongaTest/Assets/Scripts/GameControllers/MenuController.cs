using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public static MenuController instance;
    public GameObject deckList;

    private void Awake() {
        instance = this;
    }

    void Start() {
        foreach (string directory in Directory.GetDirectories(Application.dataPath + JSONPaths.path)) {
            GameObject deckItem = Instantiate(Templates.instance.DeckSelectorPrefab, deckList.transform);
            if (directory.Contains("Base"))
                deckItem.GetComponent<Toggle>().isOn = true;

            deckItem.GetComponentInChildren<Text>().text = directory.Substring(directory.LastIndexOf("/") + 1);
        }
    }


    public static void LoadDecks() {
        List<Deck> decks = new List<Deck>();
        for (int i = 0; i < instance.deckList.transform.childCount; i++) {
            Transform child = instance.deckList.transform.GetChild(i);
            if (child.GetComponent<Toggle>().isOn) {
                decks.Add(JSONObjectInterface.BuildFromJSON<Deck>(
                JSONFileInterface.RandomLine(
                    child.GetComponentInChildren<Text>().text + "/" + JSONPaths.fileName)));
            }
        }
        decks.Add(JSONObjectInterface.BuildFromJSON<Deck>(Templates.temp));
        Templates.deck = new Deck(decks);
    }
}
