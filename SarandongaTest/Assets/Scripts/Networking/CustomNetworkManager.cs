using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
public class CustomNetworkManager : NetworkManager {

    public override void OnStartServer() {
        MenuController.LoadDecks();
        base.OnStartServer();
    }
    
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
}
