using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSettings : NetworkBehaviour
{

	[SyncVar]
	public PlayerType Team;

	[SyncVar]
	public string PlayerName;
}