using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour {

	public float FlashlightRadiance = 0;

	public static List<Robber> All = new List<Robber>();

	// Use this for initialization
	void Awake () {
		All.Add(this);
	}

}
