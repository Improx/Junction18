using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigStencilMask : MonoBehaviour {

	[HideInInspector] public static BigStencilMask Instance;

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	
}
