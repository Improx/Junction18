using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour {

	public TextMeshProUGUI CountText;

	// Use this for initialization
	private void Update() {
		CountText.text = "Items stolen: " + GameManager.Instance.RobberPoints.ToString();
	}
}
