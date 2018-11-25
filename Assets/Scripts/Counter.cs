using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{

	public TextMeshProUGUI CountText;

	private void Update()
	{
		CountText.text = "Items stolen: " + GameManager.Instance.RobberPoints.ToString();
	}
}