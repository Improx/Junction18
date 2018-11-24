using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetainScreen : MonoBehaviour {

	public Image Screen;
	public TextMeshProUGUI DetainText;
    private bool _displaying;

    public static DetainScreen Instance { get; private set; }

    private void Awake() {
		Instance = this;
	}

	public void Display() {
		if (_displaying) return;
		_displaying = true;
		Debug.Log("Detained display");
		Instance.Screen.gameObject.SetActive(true);
		Screen.color *= new Color(1,1,1,0);
		DetainText.color *= new Color(1,1,1,0);
	}

    public void Hide()
    {
		_displaying = false;
		Screen.gameObject.SetActive(false);
    }
}
