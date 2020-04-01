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
        string[] deckDirectories = Directory.GetDirectories(Application.dataPath + JSONPaths.path);
        foreach (string directory in deckDirectories) {
            GameObject selector = Instantiate(Templates.instance.DeckSelectorPrefab, deckList.transform);
            if (directory.Contains("Base"))
                selector.GetComponent<Toggle>().isOn = true;

            selector.GetComponentInChildren<Text>().text = directory.Substring(directory.LastIndexOf("/") + 1);
        }
    }


    public void LoadDecks() {
        List<Deck> decks = new List<Deck>();
        for (int i = 0; i < deckList.transform.childCount; i++) {
            Transform child = deckList.transform.GetChild(i);
            if (child.GetComponent<Toggle>().isOn) {
                decks.Add(JSONObjectInterface.BuildFromJSON<Deck>(
                    JSONFileInterface.RandomLine(
                        child.GetComponentInChildren<Text>().text + "/" + JSONPaths.fileName)));
            }
        }

        Templates.deck = new Deck(decks);
    }
}
