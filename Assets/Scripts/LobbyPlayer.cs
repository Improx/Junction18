using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System;

public class LobbyPlayer : NetworkLobbyPlayer {

	[SyncVar(hook = "OnMyName")]
	public string PlayerName;
	
	[SyncVar(hook = "OnMyTeam")]
	public PlayerType PlayerTeam;
    public TextMeshProUGUI NameField;
    public TextMeshProUGUI TeamField;
	public UnityEngine.UI.Button readyButton;

	
	static Color JoinColor = new Color(255.0f/255.0f, 0.0f, 101.0f/255.0f,1.0f);
	static Color NotReadyColor = new Color(34.0f / 255.0f, 44 / 255.0f, 55.0f / 255.0f, 1.0f);
	static Color ReadyColor = new Color(0.0f, 204.0f / 255.0f, 204.0f / 255.0f, 1.0f);
	static Color TransparentColor = new Color(0, 0, 0, 0);

	public override void OnClientEnterLobby()
	{
		base.OnClientEnterLobby();

		LobbyPlayerList.Instance.AddPlayer(this);

		if (isLocalPlayer)
		{
			SetupLocalPlayer();
		}
		else
		{
			SetupOtherPlayer();
		}

		OnMyName(PlayerName);
		OnMyTeam(PlayerTeam);
	}

    public override void OnStartAuthority()
	{
		base.OnStartAuthority();

		readyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;

		SetupLocalPlayer();
	}

	void ChangeReadyButtonColor(Color c)
	{
		ColorBlock b = readyButton.colors;
		b.normalColor = c;
		b.pressedColor = c;
		b.highlightedColor = c;
		b.disabledColor = c;
		readyButton.colors = b;
	}

	void SetupLocalPlayer()
	{
		//have to use child count of player prefab already setup as "this.slot" is not set yet
		if (PlayerName == "") {
			CmdNameChanged(LobbyUIManager.Instance.playerName);
		}

		if (LobbyPlayerList.Instance.Players.Exists(x => x.PlayerTeam == PlayerType.Guard)) {
			CmdTeamChanged(PlayerType.Robber);
		} else {
			CmdTeamChanged(PlayerType.Guard);
		}

		ChangeReadyButtonColor(JoinColor);

		readyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "JOIN";
		readyButton.interactable = true;

		readyButton.onClick.RemoveAllListeners();
		readyButton.onClick.AddListener(OnReadyClicked);
	}

    public void OnReadyClicked()
	{
		SendReadyToBeginMessage();
	}
	
	public override void OnClientReady(bool readyState)
	{
		if (readyState)
		{
			ChangeReadyButtonColor(TransparentColor);

			var textComponent = readyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
			textComponent.text = "READY";
			textComponent.color = ReadyColor;
			readyButton.interactable = false;
		}
		else
		{
			ChangeReadyButtonColor(isLocalPlayer ? JoinColor : NotReadyColor);

			var textComponent = readyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
			textComponent.text = isLocalPlayer ? "JOIN" : "...";
			textComponent.color = Color.white;
			readyButton.interactable = isLocalPlayer;
		}
	}

	void SetupOtherPlayer()
	{

		ChangeReadyButtonColor(NotReadyColor);

		readyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "...";
		readyButton.interactable = false;

		OnClientReady(false);
	}

	[Command]
	public void CmdNameChanged(string name)
	{
		PlayerName = name;
	}

    [Command]
	public void CmdTeamChanged(PlayerType team)
    {
		PlayerTeam = team;
    }

	
	public void OnMyName(string newName)
	{
		PlayerName = newName;
		NameField.text = PlayerName;
	}

    private void OnMyTeam(PlayerType playerTeam)
    {
        PlayerTeam = playerTeam;

		if (PlayerTeam == PlayerType.Guard) {
			TeamField.text = "Guard";
		} else {
			TeamField.text = "Robber";
		}
    }
}