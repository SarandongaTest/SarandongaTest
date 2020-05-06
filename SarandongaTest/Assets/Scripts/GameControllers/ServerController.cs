using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class ServerController : NetworkBehaviour {
    public static Dictionary<NetworkIdentity, GameObject> players = new Dictionary<NetworkIdentity, GameObject>();
    private Dictionary<string, NetworkIdentity> currentHand = new Dictionary<string, NetworkIdentity>();
    private NetworkIdentity selector;

    public static ServerController instance;
    public Deck deck;
    public CardBlack blackCard;
    public bool selectingCard = false;

    public override void OnStartServer() {
        base.OnStartServer();
        if (isServer) {
            instance = this;
        }

        deck = Templates.deck;
        blackCard = deck.DealBlackCard();
    }

    public void AddPlayer(NetworkIdentity id, GameObject go) {
        players.Add(id, go);
        if (selector == null)
            selector = id;
    }

    public string[] DealCard(NetworkIdentity id, int count) {
        List<string> cards = new List<string>();
        for (int i = 0; i < count; i++) {
            cards.Add(JSONObjectInterface.BuildJSON<CardWhite>(deck.DealCard()));
        }

        players.TryGetValue(id, out GameObject player);
        player.GetComponent<GameController>().RpcReceiveCards(cards.ToArray());
        Debug.Log(player.name);
        return cards.ToArray();
    }

    public void PlayCard(NetworkIdentity id, string card) {
        if (selectingCard && id.netId == selector.netId) {
            currentHand.TryGetValue(card, out NetworkIdentity winner);
            selector = winner;
            GameController.instance.RpcPlayNewHand(selector);
            currentHand.Clear();
            selectingCard = false;
        } else {
            currentHand.Add(card, id);
            if (currentHand.Count >= players.Count) {
                ManageSelectCard();
            }
        }
    }

    private void ManageSelectCard() {
        selectingCard = true;
        string[] cards = new string[currentHand.Count];
        currentHand.Keys.CopyTo(cards, 0);
        players.TryGetValue(selector, out GameObject player);
        player.GetComponent<GameController>().SetSelectPlayer(cards);
    }

    public void GetBlackCard() {
        GameController.instance.RpcUpdateBlackCard(JSONObjectInterface.BuildJSON(blackCard));
    }


#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
}
