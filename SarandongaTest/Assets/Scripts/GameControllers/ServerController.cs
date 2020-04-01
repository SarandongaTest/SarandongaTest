using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class ServerController : NetworkBehaviour {
    public static List<NetworkIdentity> players = new List<NetworkIdentity>();

    public static ServerController instance;
    public Deck deck;

    public override void OnStartServer() {
        base.OnStartServer();
        if (isServer) {
            instance = this;
        }

        deck = Templates.deck;
    }
    
    public void AddPlayer(NetworkIdentity id) {
        players.Add(id);
    }
    
    public void DealCard(NetworkIdentity id) {
        GameController.instance.RpcGetCard(id ,JSONObjectInterface.BuildJSON<CardWhite>(deck.DealCard()));
    }


#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
}
