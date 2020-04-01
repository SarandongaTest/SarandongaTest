using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class CustomNetworkManager : NetworkManager {
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

    public override void OnStartServer() {
        MenuController.instance.LoadDecks();
        base.OnStartServer();
    }

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
    public override void OnServerConnect(NetworkConnection conn) {
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

    }
}
