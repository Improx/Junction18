﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour {
	
	public static GameManager Instance { get; private set; }

	public int RobberPoints = 0;
	public Text CountText;
	
	private void Update() {
		CountText.text = "Treasures stolen: " + RobberPoints.ToString();
	}

	private void Awake() {
		Instance = this;
	}

	[Command]
	public void CmdCapture(GameObject guard, GameObject robber) {
		if (guard.GetComponent<Player>().Team != PlayerType.Guard
			&& robber.GetComponent<Player>().Team != PlayerType.Robber) return;

		robber.GetComponent<Player>().RpcGetCaptured();
	}
}
