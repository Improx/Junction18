
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

	private List<PlayerSpawnPoint> _guardSpawnPoints;
	private List<PlayerSpawnPoint> _thiefSpawnPoints;
	public GameObject GuardPrefab;
	public GameObject ThiefPrefab;

	// Use this for initialization
	void Start () {
		ClientScene.RegisterPrefab(GuardPrefab);
		ClientScene.RegisterPrefab(ThiefPrefab);
	}

	public override void OnServerReady(NetworkConnection conn) {
		_guardSpawnPoints = new List<PlayerSpawnPoint>(FindObjectsOfType<PlayerSpawnPoint>()).FindAll(x => x.PlayerSpawnType == PlayerSpawnPoint.PlayerType.Guard);
		_thiefSpawnPoints = new List<PlayerSpawnPoint>(FindObjectsOfType<PlayerSpawnPoint>()).FindAll(x => x.PlayerSpawnType == PlayerSpawnPoint.PlayerType.Thief);
		base.OnServerReady(conn);
	}
	
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
		PlayerSpawnPoint spawnPoint;
		GameObject playerPrefab;
		if (conn.address == "localClient") {
        	int index = Random.Range(0, _guardSpawnPoints.Count);
			spawnPoint = _guardSpawnPoints[index];
			_guardSpawnPoints.RemoveAt(index);
			playerPrefab = GuardPrefab;
		} else {
        	int index = Random.Range(0, _thiefSpawnPoints.Count);
			spawnPoint = _thiefSpawnPoints[index];
			_thiefSpawnPoints.RemoveAt(index);
			playerPrefab = ThiefPrefab;
		}

        GameObject player = (GameObject)Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
