using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LanguageTags {

    public static LanguageTags instance;

    public LanguageTags() {
        instance = this;
    }

    public LanguageTags(string language) {
        instance = this;
        Setup(language);
    }

    public string lan = "English";
    public string decksTittle = "Decks";
    public string deckPlaceholderText = "Deck code";
    public string deckLoadButton = "Load";
    public string gamePlaceholderText = "Game name";
    public string hostGameButton = "Host game";
    public string joinGameButton = "Join game";
    public string gamesTittle = "Games";
    public string joinButton = "Join";
    public string refreshButton = "Refresh";
    public string waitingButton = "Waiting";
    public string readyButton = "Ready!";
    public string playCardButton = "Play card";
    public string selectWinnerButton = "Select winner";
    public string pickBlackCard = "Pick: ";
    public string playACardTittle = "Play a card";
    public string waitingForPlayersTittle = "Waiting for other players";
    public string selectAWinnerTittle = "Select a winner";
    public string waitingForSelectTittle = "Selecting a winner";
    public string pointsTittle = "Points: ";

    public void Setup(string language) {
        //language = "English";
        JSONObjectInterface.ExtractFromJSON(
            this,
            JSONFileInterface.ReadLine(JSONPaths.GetLanguagePath() + language + ".json", 0)
            );
    }
}
