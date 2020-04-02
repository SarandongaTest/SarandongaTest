using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class GameController : NetworkBehaviour {

    public static GameController instance;

    public GameObject blackCard;
    public GameObject decideBoard;
    private NetworkIdentity netid;

    private void Awake() {
        instance = this;
    }

    public void Start() {
        if (!isLocalPlayer) {
            gameObject.SetActive(false);
            return;
        }
        netid = GetComponent<NetworkIdentity>();
        CmdAddPlayer(netid);
        blackCard = GameObject.FindGameObjectWithTag("BlackCard");
        CmdGetBlackCard();
        CmdDeal(netid, PlayerHand.maxCards - PlayerHand.instance.hand.Count);
    }

    public void SendCard(GameObject card) {
        CmdDeal(netid, PlayerHand.maxCards - PlayerHand.instance.hand.Count);
        CmdSendCard(netid, JSONObjectInterface.BuildJSON(card.GetComponent<CardDisplayWhite>().card));
    }

    [Command]
    private void CmdSendCard(NetworkIdentity id, string card) {
        ServerController.instance.PlayCard(id, card);
    }

    [Command]
    private void CmdAddPlayer(NetworkIdentity id) {
        ServerController.instance.AddPlayer(id);
    }

    /// <summary>
    /// Instanciate a CardDisplay from a random JSON representation
    /// </summary>
    [Command]
    private void CmdDeal(NetworkIdentity id, int count) {
        ServerController.instance.DealCard(id, count);
    }

    [Command]
    private void CmdGetBlackCard() {
        ServerController.instance.GetBlackCard();
    }


    [ClientRpc]
    public void RpcGetCard(NetworkIdentity target, string[] cards) {
        if (target.netId != netId) {
            return;
        }
        
        foreach (string card in cards) {
            PlayerHand.instance.AddCard(JSONObjectInterface.BuildFromJSON<CardWhite>(card));
        }
    }

    [ClientRpc]
    public void RpcUpdateBlackCard(string card) {
        blackCard.GetComponent<CardDisplayBlack>().SetCard(JSONObjectInterface.BuildFromJSON<CardBlack>(card));
    }

    [ClientRpc]
    public void RpcSetSelectPlayer(NetworkIdentity target, string[] cards) {
        PlayerHand.instance.SelectCardTurn(true, cards);
        if (target.netId != GetComponent<NetworkIdentity>().netId) {
            return;
        } else {

        }
    }

}
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
