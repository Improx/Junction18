using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetainScreen : MonoBehaviour {

	public Image Screen;
	public TextMeshProUGUI DetainText;

    public static DetainScreen Instance { get; private set; }

    private void Awake() {
		Instance = this;
	}

	public void Display() {
		Instance.Screen.gameObject.SetActive(true);
		Screen.color *= new Color(1,1,1,0);
		DetainText.color *= new Color(1,1,1,0);
	}

    public static void Hide()
    {
		Instance.Screen.gameObject.SetActive(false);
    }
}
