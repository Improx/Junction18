using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class GameManager : NetworkBehaviour {
	
	public static GameManager Instance { get; private set; }

	public int RobberPoints = 0;

	private void Awake() {
		Instance = this;
	}

	public int NumOfRobbers => Robber.All.Count;
	public int NumOfDetainedRobbers => 0;

	public int NumOfItems => 0;
	public int NumOfStolenItems => 0;

	[Command]
	public void CmdCapture(GameObject guard, GameObject robber) {
		if (guard.GetComponent<Player>().Team != PlayerType.Guard
			&& robber.GetComponent<Player>().Team != PlayerType.Robber) return;

		robber.GetComponent<Player>().RpcGetCaptured();
	}
}
