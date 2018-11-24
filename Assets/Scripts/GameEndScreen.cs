using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndScreen : MonoBehaviour {

	public Image EndScreen;
	public TextMeshProUGUI	DetainedText;
	public TextMeshProUGUI	StolenText;

	private bool _displaying;

	public static GameEndScreen Instance { get; private set; }

	private void Awake() {
		Instance = this;
	}

    public void Display()
    {
		if (_displaying) return;
		_displaying = true;
		Debug.Log("Endscreen display");
		DetainScreen.Hide();
        EndScreen.gameObject.SetActive(true);
		EndScreen.color *= new Color(1, 1, 1, 0);
		DetainedText.text = $"Robbers detained: {GameManager.Instance.NumOfDetainedRobbers} / {GameManager.Instance.NumOfRobbers}";
		DetainedText.color *= new Color(1, 1, 1, 0);
		StolenText.text = $"Items stolen: {GameManager.Instance.NumOfStolenItems} / {GameManager.Instance.NumOfItems}";
		DetainedText.color *= new Color(1, 1, 1, 0);
    }
}
