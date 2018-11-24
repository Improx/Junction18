
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
		var playerSettings= conn.playerControllers[playerControllerId].gameObject.GetComponent<PlayerSettings>();
		GameObject playerPrefab;
		if (playerSettings.Team == PlayerType.Guard) {
			playerPrefab = GuardPrefab;
		} else {
			playerPrefab = RobberPrefab;
		}

        return (GameObject)Instantiate(playerPrefab);
    }


	private bool HasGuard() {
		foreach (var slot in lobbySlots)
		{
			if (slot) {
				if (slot.GetComponent<PlayerSettings>().Team == PlayerType.Guard) return true;
			}
		}

		return false;
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
		var hasGuard = HasGuard();
		base.OnServerAddPlayer(conn, playerControllerId);
		Debug.Log(lobbySlots);
		
		var playerSettings= conn.playerControllers[playerControllerId].gameObject.GetComponent<PlayerSettings>();
		if (!hasGuard) {
			playerSettings.Team = PlayerType.Guard;
		} else {
			playerSettings.Team = PlayerType.Robber;
		}
	}
}
