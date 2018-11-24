﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class GameManager : NetworkBehaviour {
	
	public static GameManager Instance { get; private set; }

	public int RobberPoints = 0;
	public Text CountText;
	
	private void Update() {
		if (!isLocalPlayer) return;
		
		if (NumOfRobbers != 0 && NumOfRobbers <= NumOfDetainedRobbers) {
		    foreach (var player in Players)
			{
				player.RpcDisplayEndSCreen();
			}
		}
	}

	private void Awake() {
		Instance = this;
	}

	public List<Player> Players => new List<Player>(FindObjectsOfType<Player>());

	public int NumOfRobbers => Robber.All.Count;
	public int NumOfDetainedRobbers => Robber.All.FindAll(x => x.Detained).Count;

	public int NumOfItems => 0;
	public int NumOfStolenItems => RobberPoints;

	[Command]
	public void CmdCapture(GameObject guard, GameObject robber) {
		if (guard.GetComponent<Player>().Team != PlayerType.Guard
			&& robber.GetComponent<Player>().Team != PlayerType.Robber) return;

		robber.GetComponent<Player>().RpcGetCaptured();
		robber.GetComponent<Robber>().Detained = true;
	}
}
