using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

	public float Angle = 75;
	public float MaxDistance = 5;
	public float Resolution = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void DrawFieldOfView()
	{
		Vector3 viewDir = transform.up;
		float viewDirectionAngle = Mathf.Atan2(viewDir.y, viewDir.x);
		float lowAngle = viewDirectionAngle - Angle / 2;
		float highAngle = viewDirectionAngle + Angle / 2;
		int numRays = Mathf.RoundToInt(Angle / Resolution);
		float angleIncrement = Angle / numRays;
		for (int i = 0; i < numRays; i++)
		{
			float rayAngle = lowAngle + angleIncrement * i;
			Vector3 rayDir = AngleToDirection(rayAngle);
			RaycastHit hit;
			Physics.Raycast(transform.position, viewDir, out hit);
		}
	}

	public Vector3 AngleToDirection(float worldAngle)
	{
		return new Vector3(Mathf.Cos(worldAngle), Mathf.Sin(worldAngle), 0);
	} 


}
