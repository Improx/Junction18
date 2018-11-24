using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRobber : MonoBehaviour {

	public static List<DummyRobber> All = new List<DummyRobber>();

	// Use this for initialization
	void Awake () {
		All.Add(this);
	}

}
