using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class LobbyRoom : MonoBehaviour {

    public MatchInfoSnapshot match;
    public Text matchNameText;
    public Text joinButton;

    public void Setup(MatchInfoSnapshot match) {
        this.match = match;
        matchNameText.text = match.name;
        joinButton.text = LanguageTags.instance.joinButton;
    }

    public void OnJoinButton() {
        MenuController.instance.OnJoinMatch(match);
    }
}
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
