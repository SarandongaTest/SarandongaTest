using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class LobbyPlayerController : NetworkLobbyPlayer {

    public string playerName;
    private GameObject playerObject;

    public override void OnStartClient() {
        playerObject = Instantiate(Configurations.instance.lobbyPlayerPrefab,
            MenuController.getLobbyTransform(MenuController.instance.playerLobby));
        base.OnStartClient();
    }

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        playerObject.GetComponent<LobbyPlayerObject>().Setup("My name", isLocalPlayer, this);
    }

    public bool IsReady() {
        return readyToBegin;
    }

    public void OnReady() {
        SendReadyToBeginMessage();
    }
}
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
