using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class MenuController : MonoBehaviour {
    private IEnumerator callback;


    public static MenuController instance;
    public GameObject deckList;

    public GameObject playerLobby;
    public GameObject gamesLobby;
    public GameObject nameLobby;
    public Text gameNameText;

    public InputField nameField;
    public InputField deckCodeField;
    public InputField gameNameField;
    public CustomNetworkManager lobbyManager;

    private void Awake() {
        instance = this;
    }

    void Start() {

        gamesLobby.SetActive(false);
        playerLobby.SetActive(false);
        foreach (string directory in Directory.GetDirectories(JSONPaths.GetDeckPath() + "/")) {
            GameObject deckItem = Instantiate(Configurations.instance.DeckItemPrefab, deckList.transform);
            deckItem.GetComponent<DeckItemController>().Setup(directory);
            if (directory.Contains("Base"))
                deckItem.GetComponent<Toggle>().isOn = true;
        }
    }

    public void OnSelectName() {
        if (!string.IsNullOrEmpty(nameField.text)) {
            Configurations.playerName = nameField.text;
            nameLobby.SetActive(false);
            StartCallback();
        }
    }

    private void StartCallback() {
        StartCoroutine(callback);
    }

    public void OnHostGame() {
        if (string.IsNullOrEmpty(gameNameField.text)) return;
        callback = HostGame();
        if (Configurations.playerName == "default") {
            nameLobby.SetActive(true);
        } else {
            StartCallback();
        }
    }

    public IEnumerator HostGame() {
        playerLobby.SetActive(true);
        gameNameText.text = gameNameField.text;
        lobbyManager.HostGame(gameNameField.text);
        yield return null;
    }

    public void OnJoinGame() {
        callback = JoinGame();
        if (Configurations.playerName == "default") {
            nameLobby.SetActive(true);
        } else {
            StartCallback();
        }
    }

    public IEnumerator JoinGame() {
        gamesLobby.SetActive(true);
        lobbyManager.JoinGame();
        yield return null;
    }

    public void OnRefresh() {
        lobbyManager.Refresh();
    }

    public void OnJoinMatch(MatchInfoSnapshot match) {
        lobbyManager.JoinMatch(match);
        playerLobby.SetActive(true);
        gameNameText.text = match.name;
    }

    public static Transform GetLobbyTransform(GameObject lobbyObject) {
        return lobbyObject.transform.Find("Viewport").transform.Find("Content").transform;
    }

    public void LoadDecks() {
        List<Deck> decks = new List<Deck>();
        for (int i = 0; i < deckList.transform.childCount; i++) {
            Transform child = deckList.transform.GetChild(i);
            if (child.GetComponent<Toggle>().isOn) {
                decks.Add(child.GetComponent<DeckItemController>().deck);
            }
        }
        Configurations.deck = new Deck(decks);
    }


    public void OnLoadFromWeb() {
        if (string.IsNullOrEmpty(deckCodeField.text)) return;
        switch (deckCodeField.text) {
            case "musa":
                StartCoroutine(LoadSpanish());
                break;

            default:
                StartCoroutine(OnLoadFromWeb(deckCodeField.text));
                break;
        }

        deckCodeField.text = "";
    }

    public IEnumerator LoadSpanish() {
        string path = JSONPaths.pastebinPath + "G1yXTGd8";
        CoroutineWithData coroutine = new CoroutineWithData(this, LoadJSONFromWeb(path));
        yield return coroutine.Coroutine;
        LanguageTags tags = new LanguageTags();
        JSONObjectInterface.ExtractFromJSON(tags, coroutine.result.ToString());
        deckCodeField.text = LanguageTags.instance.lan;
        MenuLanguageController.instance.Setup();
        StartCoroutine(OnLoadFromWeb("d7Z8L4R3"));
    }

    public IEnumerator OnLoadFromWeb(string code) {
        string path = JSONPaths.pastebinPath + code;
        CoroutineWithData coroutine = new CoroutineWithData(this, LoadJSONFromWeb(path));
        yield return coroutine.Coroutine;
        GameObject deckItem = Instantiate(Configurations.instance.DeckItemPrefab, deckList.transform);
        deckItem.GetComponent<DeckItemController>().SetupFromWeb(coroutine.result.ToString());
    }

    private static IEnumerator LoadJSONFromWeb(string path) {
        WWW www = new WWW(path);
        yield return www;
        if (string.IsNullOrEmpty(www.error)) {
            yield return www.text;
        } else {
            yield return null;
        }
    }
}
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
