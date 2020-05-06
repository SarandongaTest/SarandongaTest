using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class GameController : NetworkBehaviour {

    public static GameController instance;

    public GameObject blackCard;
    public GameObject decideBoard;
    private NetworkIdentity id;

    private void Awake() {
        this.name = Time.time + "";
    }

    public void Start() {
        if (isLocalPlayer) {
            name += "Local";
            instance = this;
            CmdGetBlackCard();
            CmdAddPlayer();
            CmdAskForDeal(PlayerHand.maxCards);
        }
        blackCard = GameObject.FindGameObjectWithTag("BlackCard");

    }

    public override void OnStartServer() {
        base.OnStartServer();
        id = GetComponent<NetworkIdentity>();
    }

    [Command]
    private void CmdAddPlayer() {
        Debug.Log(this.name + "- Register with id: " + id.netId);
        //Debug.Log("CmdAddPlayer " + isLocalPlayer + " " + id);
        ServerController.instance.AddPlayer(id, this.gameObject);
    }

    public void SendCard(GameObject card) {
        //if (!isLocalPlayer) return;
        CmdAskForDeal(PlayerHand.maxCards - PlayerHand.instance.hand.Count);
        CmdSendCard(JSONObjectInterface.BuildJSON(card.GetComponent<CardDisplayWhite>().card));
    }

    [Command]
    private void CmdSendCard(string card) {
        //Debug.Log("CmdSendCard " + isLocalPlayer + " " + id + " " + serverID);
        ServerController.instance.PlayCard(id, card);
    }

    /// <summary>
    /// Instanciate a CardDisplay from a random JSON representation
    /// </summary>
    [Command]
    private void CmdAskForDeal(int count) {
        Debug.Log("CmdAskForDeal: " + name + " " + count);
        /*ReceiveCards(*/ServerController.instance.DealCard(id, count)/*)*/;
    }

    [Command]
    private void CmdGetBlackCard() {
        ServerController.instance.GetBlackCard();
    }

    [ClientRpc]
    public void RpcReceiveCards(string[] cards) {
        Debug.Log("ReceiveCards: "+ isLocalPlayer);
        if (!isLocalPlayer) return;

        Debug.Log("ReceiveCards " + name + " " + isLocalPlayer + " " + cards.Length);

        /***************************************************************************************/
        PlayerHand.instance.AddCard(CardDisplayWhite.InstanciateCardDisplay(JSONObjectInterface.BuildFromJSON<CardWhite>(
            "{\"text\":\"RPCDEALCARD C" + cards.Length + " H" + PlayerHand.instance.hand.Count +" \"}"), PlayerHand.instance.gameObject));
        /***************************************************************************************/

        foreach (string card in cards) {
            PlayerHand.instance.AddCard(JSONObjectInterface.BuildFromJSON<CardWhite>(card));
        }

        if (PlayerHand.instance.hand.Count < PlayerHand.maxCards) {
            CmdAskForDeal(PlayerHand.maxCards - PlayerHand.instance.hand.Count);
            /***************************************************************************************
            PlayerHand.instance.AddCard(CardDisplayWhite.InstanciateCardDisplay(JSONObjectInterface.BuildFromJSON<CardWhite>(
                "{\"text\":\"RPCDEALCARD2 T" + target + " ID" + serverID + " C" + cards.Length + " H" + PlayerHand.instance.hand.Count + " MAX" + PlayerHand.maxCards +" \"}"), PlayerHand.instance.gameObject));
            /***************************************************************************************/
        }
    }

    [ClientRpc]
    public void RpcUpdateBlackCard(string card) {
        blackCard.GetComponent<CardDisplayBlack>().SetCard(JSONObjectInterface.BuildFromJSON<CardBlack>(card));
    }

    [Client]
    public void SetSelectPlayer(string[] cards) {
        //Debug.Log("RpcSetSelectPlayer " + isLocalPlayer + " " + target + " " + serverID);
        PlayerHand.instance.PlayCardTurn(false, cards);
        //DO THE THING
    }

    [ClientRpc]
    public void RpcPlayNewHand(NetworkIdentity id) {
        //Debug.Log("RpcPlayNewHand "/* + isLocalPlayer + " " + id.netId + " " + networkIdentity.netId*/);
        PlayerHand.instance.PlayCardTurn(true, null);
        //Volver a la normalidad
    }

}
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
