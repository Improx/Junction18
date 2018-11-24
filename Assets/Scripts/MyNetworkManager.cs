
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkLobbyManager {
	public GameObject GuardPrefab;
	public GameObject RobberPrefab;

	// Use this for initialization
	void Start () {
		ClientScene.RegisterPrefab(GuardPrefab);
		ClientScene.RegisterPrefab(RobberPrefab);
	}

	public override void OnLobbyServerSceneChanged(string scene) {
		base.OnLobbyServerSceneChanged(scene);
	}
	
    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
		GameObject playerPrefab;
		if (0 == playerControllerId) {
			playerPrefab = GuardPrefab;
		} else {
			playerPrefab = RobberPrefab;
		}

        return (GameObject)Instantiate(playerPrefab);
    }
}
