using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayerList : MonoBehaviour {

	public RectTransform PlayerInfoContent;

	public static LobbyPlayerList Instance { get; private set; }

	public List<LobbyPlayer> Players = new List<LobbyPlayer>();

	private void Awake() {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
	}

	public void AddPlayer(LobbyPlayer player)
	{
		if (Players.Contains(player))
			return;

		Players.Add(player);

		player.transform.SetParent(PlayerInfoContent, false);
	}

	public void RemovePlayer(LobbyPlayer player)
	{
		Players.Remove(player);
	}
}
