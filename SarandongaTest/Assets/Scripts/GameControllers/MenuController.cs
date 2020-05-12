using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public static MenuController instance;
    public GameObject deckList;

    public InputField gameNameField;
    public CustomNetworkManager lobbyManager;

    public GameObject playerLobby;
    public GameObject gamesLobby;
    public Text gameNameText;

    private void Awake() {
        instance = this;
    }

    void Start() {
        gamesLobby.SetActive(false);
        playerLobby.SetActive(false);
        foreach (string directory in Directory.GetDirectories(Application.dataPath + JSONPaths.decksPath)) {
            GameObject deckItem = Instantiate(Configurations.instance.DeckSelectorPrefab, deckList.transform);
            if (directory.Contains("Base"))
                deckItem.GetComponent<Toggle>().isOn = true;

            deckItem.GetComponentInChildren<Text>().text = directory.Substring(directory.LastIndexOf("/") + 1);
        }
    }

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
    public void OnJoinMatch(MatchInfoSnapshot match) {
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        lobbyManager.JoinMatch(match);
        playerLobby.SetActive(true);
        gameNameText.text = match.name;
    }

    public static Transform getLobbyTransform(GameObject lobbyObject) {
        return lobbyObject.transform.Find("Viewport").transform.Find("Content").transform;
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
        decks.Add(JSONObjectInterface.BuildFromJSON<Deck>(Configurations.temp));
        Configurations.deck = new Deck(decks);
    }

    public void OnHostGame() {
        if (string.IsNullOrEmpty(gameNameField.text)) return;
        playerLobby.SetActive(true);
        gameNameText.text = gameNameField.text;
        lobbyManager.HostGame(gameNameField.text);
    }

    public void OnJoinGame() {
        gamesLobby.SetActive(true);
        lobbyManager.JoinGame();
    }
}
