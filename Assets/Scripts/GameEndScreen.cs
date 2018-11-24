using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndScreen : MonoBehaviour {

	public GameObject EndScreen;
	public TextMeshProUGUI	DetainedText;
	public TextMeshProUGUI	StolenText;

	public static GameEndScreen Instance { get; private set; }

	private void Awake() {
		Instance = this;
	}

    public void Display()
    {
        EndScreen.SetActive(true);
		DetainedText.text = $"Robbers detained: {GameManager.Instance.NumOfDetainedRobbers} / {GameManager.Instance.NumOfRobbers}";
		StolenText.text = $"Items stolen: {GameManager.Instance.NumOfStolenItems} / {GameManager.Instance.NumOfItems}";

    }
}
