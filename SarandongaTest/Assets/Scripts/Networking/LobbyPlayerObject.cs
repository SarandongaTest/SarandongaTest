using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerObject : MonoBehaviour {

    private LobbyPlayerController controller;
    public Text playerNameText;
    public Button playerButton;
    public Text buttonText;

    public void Setup(string name, bool isLocalPlayer, LobbyPlayerController controller) {
        if (isLocalPlayer) {
            playerButton.interactable = true;
            playerNameText.text = name;
        }
        this.controller = controller;
    }

    public void OnReadyClicked() {
        controller.OnReady();
    }

    private void Update() {
        if (controller != null) {
            buttonText.text = controller.IsReady() ? "Ready" : "Waiting";
        }
    }
}
