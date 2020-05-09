using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class CustomNetworkManager : NetworkLobbyManager {

    public GameObject lobbyRoomPrefab;

    public override void OnStartServer() {
        MenuController.LoadDecks();
        base.OnStartServer();
    }

    public override void OnClientConnect(NetworkConnection conn) {
        base.OnClientConnect(conn);
    }

    public void HostGame(string gameName) {
        StartMatchMaker();
        matchMaker.CreateMatch(gameName, (uint) maxPlayers, true, "", "", "", 0, 0, OnMatchCreate);
    }

    public void JoinGame() {
        StartMatchMaker();
        matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
    }

    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) {
        if (!success) return;

        base.OnMatchList(success, extendedInfo, matchList);

        foreach (MatchInfoSnapshot match in matchList) {
            GameObject lobbyRoom = Instantiate(lobbyRoomPrefab, MenuController.getLobbyTransform(MenuController.instance.gamesLobby));
            lobbyRoom.GetComponent<LobbyRoom>().Setup(match);
        }
    }

    internal void JoinMatch(MatchInfoSnapshot match) {
        matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);
    }

    public override void OnLobbyServerPlayersReady() {
        //See all the player?
        Configurations.players = numPlayers;
        base.OnLobbyServerPlayersReady();
    }

    /*public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer) {
        //gamePlayer.GetComponent<GameController>().playerName = lobbyPlayer.GetComponent<LobbyPlayerController>().playerName;
        return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);
    }*/
    

#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
}
