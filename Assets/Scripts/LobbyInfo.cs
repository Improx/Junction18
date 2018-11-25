using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using TMPro;

public class LobbyInfo : MonoBehaviour {
	
	public TextMeshProUGUI NameText;
	public TextMeshProUGUI SlotsText;

	private MatchInfoSnapshot _matchInfo = null;
	
	public void JoinLobby() {
		MyNetworkManager.Instance.matchMaker.JoinMatch(_matchInfo.networkId, "", "", "", 0, 0, MyNetworkManager.Instance.OnMatchJoined);
		MyNetworkManager.Instance.backDelegate = MyNetworkManager.Instance.StopClientClbk;
	}

    internal void SetInfo(MatchInfoSnapshot matchInfoSnapshot)
    {
		_matchInfo = matchInfoSnapshot;
		NameText.text = matchInfoSnapshot.name;
		SlotsText.text = $"{matchInfoSnapshot.currentSize} / {matchInfoSnapshot.maxSize}";
    }
}
