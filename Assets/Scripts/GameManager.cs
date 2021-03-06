﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{

	public static GameManager Instance { get; private set; }

	[SyncVar]
	public int RobberPoints = 0;
	public Text CountText;

	public List<GameObject> ItemPrefabs;

	private void Update()
	{
		if (!isServer) return;
		if ((NumOfRobbers != 0 && NumOfRobbers <= NumOfDetainedRobbers) ||
			(NumOfItems <= NumOfStolenItems))
		{
			foreach (var player in Players)
			{
				player.RpcDisplayEndSCreen();
			}
		}
	}

	private void Awake()
	{
		Instance = this;
	}

	public List<Player> Players => new List<Player>(FindObjectsOfType<Player>());

	public int NumOfRobbers => Robber.All.Count;
	public int NumOfDetainedRobbers => Robber.All.FindAll(x => x.Detained).Count;

	public int NumOfItems => 3;
	public int NumOfStolenItems => RobberPoints;

	[Command]
	public void CmdCapture(GameObject guard, GameObject robber)
	{
		if (guard.GetComponent<Player>().Team != PlayerType.Guard &&
			robber.GetComponent<Player>().Team != PlayerType.Robber) return;

		robber.GetComponent<Player>().RpcGetCaptured();
		robber.GetComponent<Robber>().Detained = true;
	}
}
