using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quickmove : MonoBehaviour {

	public float speed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis("Horizontal")*speed*Time.deltaTime;
        var y = Input.GetAxis("Vertical")* speed *Time.deltaTime;
        transform.Translate(x, y, 0);
	}
}
