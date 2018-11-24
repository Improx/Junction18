using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMoveAndRotate : MonoBehaviour {

	public float MoveSpeed = 3;
	public float RotationSpeed = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += MoveSpeed * moveVec * Time.deltaTime;
		float rotationAmount = -Input.GetAxis("Mouse X");
		transform.RotateAroundLocal(Vector3.forward, rotationAmount * RotationSpeed * Time.deltaTime);
	}


}
