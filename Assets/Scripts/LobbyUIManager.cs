using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Match;

public class LobbyUIManager : MonoBehaviour {

	public GameObject FindMenu;
	public GameObject LobbyMenu;
	public GameObject Background;
	public TMP_InputField playerNameText;
	public TMP_InputField roomNameText;

	public RectTransform RoomInfoContent;
	public LobbyPlayerList PlayerList;
	public GameObject RoomInfoPrefab;
	public GameObject PlayerInfoPrefab;

	public MyNetworkManager lobbyManager;
    public string playerName = "";

    public static LobbyUIManager Instance { get; private set; }

	

	private void Awake() {
		Instance = this;
	}

	private void Start() {
		lobbyManager.StartMatchMaker();
		RefreshList();
	}
	public void CreateRoom() {
		lobbyManager.StartMatchMaker();
		var status = lobbyManager.matchMaker.CreateMatch(
			roomNameText.text,
			(uint)lobbyManager.maxPlayers,
			true,
			"", "", "", 0, 0,
			lobbyManager.OnMatchCreate);
		lobbyManager.backDelegate = lobbyManager.StopHostClbk;
		lobbyManager._isMatchmaking = true;
	}

	public void RefreshList() {
		lobbyManager.matchMaker.ListMatches(0,6, "", true, 0, 0, OnGUIMatchList);
	}

	public void OnGUIMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		if (matches.Count == 0)
		{
			return;
		}

		foreach (Transform t in RoomInfoContent)
			Destroy(t.gameObject);

		for (int i = 0; i < matches.Count; ++i)
		{
			GameObject o = Instantiate(RoomInfoPrefab) as GameObject;

			var info  = o.GetComponent<LobbyInfo>();
			info.SetInfo(matches[i]);
			o.transform.SetParent(RoomInfoContent, false);
		}
	}

    public static void OpenLobby() {
		Instance.Background.SetActive(true);
		if (Instance.playerNameText) {
			Instance.playerName = Instance.playerNameText.text;
		}
		Instance.FindMenu.SetActive(false);
		Instance.LobbyMenu.SetActive(true);
    }
    public static void CloseLobby() {
		Instance.Background.SetActive(true);
		Instance.FindMenu.SetActive(true);
		Instance.LobbyMenu.SetActive(false);
    }

	public static void CloseCanvas() {
		Instance.Background.SetActive(false);
		Instance.FindMenu.SetActive(false);
		Instance.LobbyMenu.SetActive(false);
	}

	public void Disconnect() {
		DestroyImmediate(MyNetworkManager.Instance.gameObject);
		SceneChanger.QuickChangeScene("MainMenu");
	}
}
