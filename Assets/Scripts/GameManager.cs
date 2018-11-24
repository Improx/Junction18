using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {
	
	public static GameManager Instance { get; private set; }

	

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
