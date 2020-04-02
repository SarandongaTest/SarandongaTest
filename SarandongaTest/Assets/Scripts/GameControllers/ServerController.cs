using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class ServerController : NetworkBehaviour {
    public static List<NetworkIdentity> players = new List<NetworkIdentity>();
    private Dictionary<string, NetworkIdentity> currentHand = new Dictionary<string, NetworkIdentity>();
    private int selector = 0;

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

    public void AddPlayer(NetworkIdentity id) {
        players.Add(id);
    }

    public void DealCard(NetworkIdentity id, int count) {
        List<string> cards = new List<string>();
        for (int i = 0; i < count; i++) {
            cards.Add(JSONObjectInterface.BuildJSON<CardWhite>(deck.DealCard()));
        }

        GameController.instance.RpcGetCard(id, cards.ToArray());
    }

    public void PlayCard(NetworkIdentity id, string card) {
        if (selectingCard) {
            NetworkIdentity winner;
            currentHand.TryGetValue(card, out winner);
        }
        currentHand.Add(card, id);
        if (currentHand.Count >= players.Count) {
            ManageSelectCard();
        }
    }

    private void ManageSelectCard() {
        string[] cards = new string[currentHand.Count];
        currentHand.Keys.CopyTo(cards, 0);
        selectingCard = true;
        GameController.instance.RpcSetSelectPlayer(players[selector], cards);
        selector++;
        selector %=  players.Count;
    }

    public void GetBlackCard() {
        GameController.instance.RpcUpdateBlackCard(JSONObjectInterface.BuildJSON(blackCard));
    }


#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
}
