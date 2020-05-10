using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class GameController : NetworkBehaviour {

    public static GameController instance;
    public string playerName;

    public GameObject blackCard;
    public GameObject decideBoard;
    private NetworkIdentity id;

    private void Awake() {
        this.name = Time.time + "";
    }

    public void Start() {
        blackCard = GameObject.FindGameObjectWithTag("BlackCard");
        if (isLocalPlayer) {
            name += "Local";
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
        //RpcSetup();
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
        ServerController.instance.GetBlackCard();
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
    public void RpcSetSelectPlayer() {
        if (!isLocalPlayer) return;
        PlayerHand.instance.TriggerPlayCardTurn(false);
    }

    [ClientRpc]
    public void RpcSelectCard() {
        if (!isLocalPlayer) return;
        PlayerHand.instance.DecideCard();
    }

    [ClientRpc]
    public void RpcGetPlayedCard(string card) {
        if (!isLocalPlayer) return;
        PlayerHand.instance.AddSelectionCard(card);
    }

    [ClientRpc]
    public void RpcProceedToDecide() {
        if (!isLocalPlayer) {
            //X player is deciding
        }
    }

    [ClientRpc]
    public void RpcPlayNewHand(NetworkIdentity id) {
        PlayerHand.instance.TriggerPlayCardTurn(true);
        //Volver a la normalidad
    }

}
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
