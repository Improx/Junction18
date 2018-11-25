using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

	[SerializeField]
	private float _secondsOfTime = 150f;
	[SerializeField]
	private TextMeshProUGUI _timerText;

	private void Update()
	{
		if (_secondsOfTime <= 0f) return;

		_secondsOfTime -= Time.deltaTime;

		TimeSpan time = TimeSpan.FromSeconds(_secondsOfTime);
		_timerText.text = time.ToString(@"mm\:ss");

		if (_secondsOfTime <= 0f)
		{
			GameEndScreen.Instance.Display();
		}
	}
}