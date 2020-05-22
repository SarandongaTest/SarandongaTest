using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class LobbyPlayerController : NetworkLobbyPlayer {

    [SyncVar (hook = "OnNameChanged")]
    public string playerName;
    private GameObject playerObject;


    public override void OnStartClient() {
        base.OnStartClient();
        playerObject = Instantiate(Configurations.instance.lobbyPlayerPrefab,
            MenuController.GetLobbyTransform(MenuController.instance.playerLobby));
        playerObject.GetComponent<LobbyPlayerObject>().UpdateNameText(playerName);

    }

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        playerObject.GetComponent<LobbyPlayerObject>().Setup(isLocalPlayer, this);
        playerName = Configurations.playerName;
        CmdChangeName(playerName);
    }

    private void Start() {
        DontDestroyOnLoad(this);
        //CmdAskForName(SystemInfo.deviceName);
    }

    public void OnNameChanged(string newName) {
        playerName = newName;
        playerObject.GetComponent<LobbyPlayerObject>().UpdateNameText(playerName);
    }

    public bool IsReady() {
        return readyToBegin;
    }

    public void OnReady() {
        SendReadyToBeginMessage();
    }

    /*[Command]
    public void CmdAskForName(string a) {
        Debug.Log(a + isLocalPlayer);
        RpcAskForName();
    }

    [ClientRpc]
    private void RpcAskForName() {
        if (!isLocalPlayer) return;
        Debug.Log("Asked for name");
        CmdChangeName(Configurations.playerName);
    }

    [ClientRpc]
    public void RpcExtendName(string newName) {
        playerName = newName;
        playerObject.GetComponent<LobbyPlayerObject>().UpdateNameText(playerName);

    }*/

    [Command]
    public void CmdChangeName(string newName) {
        //RpcExtendName(newName);
        playerName = newName;
    }
}
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
