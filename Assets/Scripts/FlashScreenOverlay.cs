using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScreenOverlay : MonoBehaviour {

	private static FlashScreenOverlay _instance;
	private Image _image;

	// Use this for initialization
	void Start () {
		_instance = this;
		_image = GetComponent<Image>();
	}

	public static void SetAmount(float amount)
	{
		Color color = _instance._image.color;
		color.a = amount;
		_instance._image.color = color;
	}
}
