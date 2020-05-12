using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class ServerController : NetworkBehaviour {
    public Dictionary<NetworkIdentity, GameObject> players = new Dictionary<NetworkIdentity, GameObject>();
    private Dictionary<string, NetworkIdentity> currentHand = new Dictionary<string, NetworkIdentity>();
    private NetworkIdentity selector;

    public static ServerController instance;
    public Deck deck;
    public CardBlack blackCard;
    public bool selectingCard = false;

    private void Start() {
        if (!isServer) return;
        instance = this;
    }

    public override void OnStartServer() {
        base.OnStartServer();
        if (isServer) {
            instance = this;
            deck = Configurations.deck;
            do {
                blackCard = deck.DealBlackCard();
            } while (blackCard.pick != 1);
        }
    }

    public void AddPlayer(NetworkIdentity id, GameObject go) {
        players.Add(id, go);

        if (selector == null) {
            selector = id;
            GetGameController(go).RpcSetPlayCardTurn(false); ;
        }
    }

    public void DealCard(NetworkIdentity id, int count) {
        //Building cards list
        List<string> cards = new List<string>();
        for (int i = 0; i < count; i++) {
            cards.Add(JSONObjectInterface.BuildJSON(deck.DealCard()));
        }

        //Getting the player object to send the list
        players.TryGetValue(id, out GameObject player);
        GetGameController(player).RpcReceiveCards(cards.ToArray());
    }

    public void PlayCard(NetworkIdentity id, string card) {
        //Manage exceptions
        if (!selectingCard && !currentHand.ContainsValue(id)) {
            currentHand.Add(card, id);
            SendCardToSelector(card);
            if (currentHand.Count >= players.Count - 1) {
                ManageSelectCard();
            }
        }

        //Select winner
        if (selectingCard && id.netId == selector.netId) {
            //Last selector is now in play card turn
            players.TryGetValue(selector, out GameObject selectorObject);
            GetGameController(selectorObject).RpcSetPlayCardTurn(true);

            //Update selector and clear hand
            currentHand.TryGetValue(card, out selector);
            currentHand.Clear();
            selectingCard = false;

            //Telling new selector
            players.TryGetValue(selector, out selectorObject);
            GetGameController(selectorObject).RpcSetPlayCardTurn(false);

            //The server tell everyone to set interactable the play button if active
            GameController.instance.RpcPlayNewHand();

            //Update black card
            do {
                blackCard = deck.DealBlackCard();
            } while (blackCard.pick != 1);
            UpdateBlackCard();
        }
    }

    private void SendCardToSelector(string card) {
        players.TryGetValue(selector, out GameObject player);
        GetGameController(player).RpcGetPlayedCard(card);
    }

    private void ManageSelectCard() {
        selectingCard = true;
        players.TryGetValue(selector, out GameObject player);
        GetGameController(player).RpcSelectCard();
    }

    public void UpdateBlackCard() {
        GameController.instance.RpcUpdateBlackCard(JSONObjectInterface.BuildJSON(blackCard));
    }

    private GameController GetGameController(GameObject gameObject) {
        return gameObject.GetComponent<GameController>();
    }


#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
}
