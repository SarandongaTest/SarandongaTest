using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerObject : MonoBehaviour {

    private LobbyPlayerController controller;
    public Text playerNameText;
    public Button playerButton;
    public Text buttonText;

    public void Setup(bool isLocalPlayer, LobbyPlayerController controller) {
        buttonText.text = LanguageTags.instance.waitingButton;
        if (isLocalPlayer) {
            playerButton.interactable = true;
        }
        this.controller = controller;
    }

    public void OnReadyClicked() {
        controller.OnReady();
        buttonText.text = controller.IsReady() ? LanguageTags.instance.readyButton : LanguageTags.instance.waitingButton;
    }

    public void UpdateNameText(string newName) {
        playerNameText.text = newName;
    }
}
