using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class GameController : NetworkBehaviour {

    public static GameController instance;
    [SyncVar]
    public string playerName;

    public GameObject blackCard;
    public GameObject decideBoard;
    private NetworkIdentity id;

    public void Start() {
        name = playerName;
        blackCard = GameObject.FindGameObjectWithTag("BlackCard");
        if (isLocalPlayer) {
            instance = this;
            CmdSetup();
        }
    }

    public override void OnStartServer() {
        base.OnStartServer();
        id = GetComponent<NetworkIdentity>();
    }

    [Command]
    public void CmdSetup() {
        if (ServerController.instance == null) {
            Invoke("CmdSetup", 1);
            return;
        }
        CmdAddPlayer();
        CmdGetBlackCard();
        CmdAskForDeal(PlayerHand.maxCards);
    }

    [Command]
    private void CmdAddPlayer() {
        ServerController.instance.AddPlayer(id, this.gameObject);
    }

    public void SendCard(GameObject card) {
        CmdAskForDeal(PlayerHand.maxCards - PlayerHand.instance.hand.Count);
        CmdSendCard(JSONObjectInterface.BuildJSON(card.GetComponent<CardDisplayWhite>().card));
    }

    [Command]
    private void CmdSendCard(string card) {
        ServerController.instance.PlayCard(id, card);
    }

    /// <summary>
    /// Instanciate a CardDisplay from a random JSON representation
    /// </summary>
    [Command]
    private void CmdAskForDeal(int count) {
        ServerController.instance.DealCard(id, count);
    }

    [Command]
    private void CmdGetBlackCard() {
        ServerController.instance.UpdateBlackCard();
    }

    [ClientRpc]
    public void RpcReceiveCards(string[] cards) {
        if (!isLocalPlayer) return;

        foreach (string card in cards) {
            PlayerHand.instance.AddCard(JSONObjectInterface.BuildFromJSON<CardWhite>(card));
        }

        if (PlayerHand.instance.hand.Count < PlayerHand.maxCards) {
            CmdAskForDeal(PlayerHand.maxCards - PlayerHand.instance.hand.Count);
        }
    }

    [ClientRpc]
    public void RpcUpdateBlackCard(string card) {
        blackCard.GetComponent<CardDisplayBlack>().SetCard(JSONObjectInterface.BuildFromJSON<CardBlack>(card));
    }

    [ClientRpc]
    public void RpcStartupSetSelectTurn() {
        if (!isLocalPlayer) return;
        PlayerHand.instance.BasicPlayCardTurn();
    }

    [ClientRpc]
    public void RpcSetPlayCardTurn(bool playCardTurn) {
        if (!playCardTurn) {
            if (isLocalPlayer) {
                PopupController.instance.Setup("You won");
            } else {
                PopupController.instance.Setup(name + " won");
            }
        }
        if (!isLocalPlayer) return;
        PlayerHand.instance.TriggerPlayCardTurn(playCardTurn);
    }

    [ClientRpc]
    public void RpcProceedToDecide() {
        UIController.instance.SetSelectingTittle();
        if (!isLocalPlayer) return;
        PlayerHand.instance.ProceedToDecideCard();
    }

    [ClientRpc]
    public void RpcGetPlayedCard(string card) {
        if (!isLocalPlayer) return;
        PlayerHand.instance.AddSelectionCard(card);
    }

    [ClientRpc]
    public void RpcPlayNewHand() {
        PlayerHand.instance.PlayNewHand();
    }

}
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
