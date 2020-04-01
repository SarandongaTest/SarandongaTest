using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class GameController : NetworkBehaviour {

    public static GameController instance;

    public GameObject blackCard;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        blackCard = GameObject.FindGameObjectWithTag("BlackCard");
        Init();
    }

    public void Init() {
            CmdDeal(GetComponent<NetworkIdentity>());
        /*blackCard.GetComponent<CardDisplayBlack>().SetCard(deck.DealBlackCard());*/
    }

    public override void OnStartClient() {
        CmdAddPlayer(GetComponent<NetworkIdentity>());
    }

    /// <summary>
    /// Instanciate a CardDisplay from a random JSON representation
    /// </summary>
    [Command]
    public void CmdDeal(NetworkIdentity id) {
        ServerController.instance.DealCard(id);
    }

    [Command]
    private void CmdAddPlayer(NetworkIdentity id) {
        ServerController.instance.AddPlayer(id);
    }

    [ClientRpc]
    public void RpcGetCard(NetworkIdentity target, string card) {
        if (target.netId != GetComponent<NetworkIdentity>().netId) {
            return;
        }
        PlayerHand.instance.AddCard(
            CardDisplayWhite.InstanciateCardDisplay(JSONObjectInterface.BuildFromJSON<CardWhite>(card),
            Templates.instance.whiteCardPrefab,
            PlayerHand.instance.gameObject));

        if (PlayerHand.instance.hand.Count < 10) {
            Init();
        }

    }
}
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
