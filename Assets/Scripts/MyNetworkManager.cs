
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

public class MyNetworkManager : NetworkLobbyManager {
	public GameObject GuardPrefab;
	public GameObject RobberPrefab;

	public static MyNetworkManager Instance;

	protected ulong _currentMatchID;
	
    public bool _isMatchmaking = false;
    public bool _disconnectServer = false;
	public delegate void BackButtonDelegate();
	public BackButtonDelegate backDelegate;

	private void Awake() {
		networkAddress = "127.0.0.1";
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		ClientScene.RegisterPrefab(GuardPrefab);
		ClientScene.RegisterPrefab(RobberPrefab);
	}
                 
	public void StopHostClbk()
	{
		if (_isMatchmaking)
		{
			matchMaker.DestroyMatch((NetworkID)_currentMatchID, 0, OnDestroyMatch);
			_disconnectServer = true;
		}
		else
		{
			StopHost();
		}

		
		LobbyUIManager.CloseLobby();
	}

	public override void OnDestroyMatch(bool success, string extendedInfo)
	{
		base.OnDestroyMatch(success, extendedInfo);
		if (_disconnectServer)
		{
			StopMatchMaker();
			StopHost();
			NetworkServer.Shutdown();
		}
	}

	public void StopClientClbk()
	{
		StopClient();

		if (_isMatchmaking)
		{
			StopMatchMaker();
		}

		LobbyUIManager.CloseLobby();
	}

	public void StopServerClbk()
	{
		StopServer();
		LobbyUIManager.CloseLobby();
	}
	
	
    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
		var team = conn.playerControllers[playerControllerId].gameObject.GetComponent<LobbyPlayer>().PlayerTeam;
		
		GameObject prf;
		if (team == PlayerType.Guard) {
			prf = GuardPrefab;
		} else {
			prf = RobberPrefab;
		}

		LobbyUIManager.CloseCanvas();
        return (GameObject)Instantiate(prf);
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

	/*public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
		var hasGuard = HasGuard();
		base.OnServerAddPlayer(conn, playerControllerId);
		
		var playerSettings= conn.playerControllers[playerControllerId].gameObject.GetComponent<PlayerSettings>();
		if (!hasGuard) {
			playerSettings.Team = PlayerType.Guard;
		} else {
			playerSettings.Team = PlayerType.Robber;
		}
	}*/

	
	public override void OnStartHost()
	{
		base.OnStartHost();
		
		backDelegate = StopHostClbk;
		LobbyUIManager.OpenLobby();

	}

	public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo) {
			base.OnMatchCreate(success, extendedInfo, matchInfo);
            _currentMatchID = (System.UInt64)matchInfo.networkId;
	}


	// Client
	public override void OnClientConnect(NetworkConnection conn)
	{
		base.OnClientConnect(conn);

		//if (!NetworkServer.active)
		{
			backDelegate = StopClientClbk;
			LobbyUIManager.OpenLobby();
		}
	}
	
	public override void OnClientDisconnect(NetworkConnection conn)
	{
		base.OnClientDisconnect(conn);
		LobbyUIManager.CloseLobby();
	}

	public override void OnClientError(NetworkConnection conn, int errorCode)
	{
		LobbyUIManager.CloseLobby();
	}
}
